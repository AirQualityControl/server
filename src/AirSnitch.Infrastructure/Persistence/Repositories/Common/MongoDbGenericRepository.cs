using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.Extensions;
using AirSnitch.Infrastructure.Persistence.Query;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Repositories.Common
{
    /// <inheritdoc/>
    internal sealed class MongoDbGenericRepository<TEntity> : IGenericRepository<TEntity>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoDbGenericRepository(MongoDbClient client, string collectionName = default)
        {
            _collection = client.Db.GetCollection<TEntity>(
                collectionName ?? typeof(TEntity).Name.ToLowerCamelCase());
        }

        /// <inheritdoc/>
        public async Task SaveAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        
        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var filter = Builders<TEntity>.Filter.Where(predicate);
            return await _collection.Find(filter).ToListAsync();
        }
        
        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<TProjection>> GetByAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjection>> projection)
        {
            var filter = Builders<TEntity>.Filter.Where(predicate);
            return await _collection.Find(filter).Project(projection).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<QueryResult> ExecuteQueryAsync(IQuery query)
        {
            var mongoQuery = (MongoDbQuery) query;
            
            var bsonDocuments = await _collection
                .Find(mongoQuery.Filter)
                .Skip(mongoQuery.PageOptions.ItemsToSkip)
                .Limit(mongoQuery.PageOptions.ItemsLimit)
                .Project(mongoQuery.Projection)
            .ToListAsync();

            return new QueryResult(
                bsonDocuments, 
                new MongoDbQueryResultFormatter(),
                mongoQuery.PageOptions
            );
        }

        public async Task<DeletionResult> DeleteOneBy(Expression<Func<TEntity, bool>> filter)
        {
            var deleteResult = await _collection.DeleteOneAsync(filter);

            if (deleteResult.IsAcknowledged)
            {
                return deleteResult.DeletedCount > 0 ? DeletionResult.Success : DeletionResult.NotFound;
            }
            //TODO: think about write concern and eventual consistency 
            throw new Exception("");
        }

        public async Task<DeletionResult> DeleteBy(Expression<Func<TEntity, bool>> filter)
        {
            //TODO: think about write concern and eventual consistency
            await _collection.DeleteManyAsync(filter);
            return DeletionResult.Success;
        }

        /// <summary>
        ///     Asynchronously update a whole entity by specify entity member
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="entityMemberSelector">Entity member selector</param>
        /// <param name="memberValue">Entity member value</param>
        /// <returns>Task</returns>
        public async Task UpdateByAsync<TMember>(TEntity entity, Expression<Func<TEntity, TMember>> entityMemberSelector, 
            TMember memberValue) 
            where TMember : IEquatable<TMember>
        {
            var filter = Builders<TEntity>.Filter.Eq(entityMemberSelector, memberValue);
            await _collection.ReplaceOneAsync(filter, entity);
        }
        
        public Task<long> Count {
            get
            {
                var query = _collection.Find(x => true);
                var totalNumberOfDocuments = query.CountDocumentsAsync();
                return totalNumberOfDocuments;
            }
        }
    }
}