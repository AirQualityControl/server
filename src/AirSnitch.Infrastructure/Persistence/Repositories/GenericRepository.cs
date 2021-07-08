using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    {
        public Task<TEntity> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TProjection>> GetByAsync<TProjection>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProjection>> projection)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            var mongoQuery = MongoDbQuery.CreateFromScheme(queryScheme);
            return null;
        }
    }
}