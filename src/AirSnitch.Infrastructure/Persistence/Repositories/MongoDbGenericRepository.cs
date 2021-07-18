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

namespace AirSnitch.Infrastructure.Persistence.Repositories
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
        public Task<TEntity> FindByIdAsync(string id)
        {
            return null;
        }
        
        /// <inheritdoc/>
        public Task<TEntity> GetById(string id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task SaveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public Task<IReadOnlyCollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public Task<IReadOnlyCollection<TProjection>> GetByAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjection>> projection)
        {
            throw new NotImplementedException();
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
                MongoDbQueryResultEntry.BuildFrom(bsonDocuments), 
                mongoQuery.PageOptions
            );
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