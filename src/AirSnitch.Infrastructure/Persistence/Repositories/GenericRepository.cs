using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    /// <inheritdoc/>
    public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity>
    {
        private readonly MongoDbClient _client;
        private readonly IMongoCollection<TEntity> _collection;
        private string _collectionName;
        
        public GenericRepository(MongoDbClient client)
        {
            _client = client;
            _collection = client.Db.GetCollection<TEntity>(
                _collectionName ?? typeof(TEntity).Name.ToLowerCamelCase());
        }

        public void SetCollectionName(string collectionName)
        {
            _collectionName = collectionName;
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
        public async Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            var query = MongoDbQuery.CreateFromScheme(queryScheme);

            var bsonDocuments = await _collection
                .Find(query.Filter)
                .Project(query.Projection)
                .ToListAsync();
            
            var jsonData = bsonDocuments.Select(r => r.ToJson()).ToList();

            return new QueryResult(jsonData, queryScheme.PageOptions)
            {
                IsSuccess = true,
            };
        }
    }
}