using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirSnitch.Core.Infrastructure.Persistence;
using AirSnitch.Infrastructure.Persistence.Extenstion;


using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    /// <inheritdoc cref="IAirMonitoringStationRepository" />
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected IMongoCollection<TEntity> Collection { get; }
        
        protected virtual string CollectionName { get; }

        protected BaseRepository()
        {
            Collection =
                BaseMongoDbClient.Db.GetCollection<TEntity>(CollectionName ?? typeof(TEntity).Name.ToLowerCamelCase());
        }

        ///<inheritdoc/>
        public async Task SaveAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        ///<inheritdoc/>
        public async Task<IReadOnlyCollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Contract.Requires(predicate != null);
            
            var filter = Builders<TEntity>.Filter.Where(predicate);
            return await Collection.Find(filter).ToListAsync();
        }

        ///<inheritdoc/>
        public async Task<IReadOnlyCollection<TProjection>> GetByAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, 
            Expression<Func<TEntity, TProjection>> projection)
        {
            Contract.Requires(predicate != null);
            
            var filter = Builders<TEntity>.Filter.Where(predicate);
            return await Collection.Find(filter).Project(projection).ToListAsync();
        }

        /// <summary>
        ///     Asynchronously update a whole entity by specify entity member
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="entityMemberSelector">Entity member selector</param>
        /// <param name="memberValue">Entity member value</param>
        /// <returns>Task</returns>
        protected async Task UpdateByAsync<TMember>(TEntity entity, Expression<Func<TEntity, TMember>> entityMemberSelector, 
            TMember memberValue) 
            where TMember : IEquatable<TMember>
        {
            var filter = Builders<TEntity>.Filter.Eq(entityMemberSelector, memberValue);
            await Collection.ReplaceOneAsync(filter, entity);
        }
    }
}
