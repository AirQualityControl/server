using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AirSnitch.Core.Infrastructure.Persistence
{
    /// <summary>
    ///     Interface that represent a set of basic operation for working with persistence data
    /// </summary>
    /// <typeparam name="TEntity">Entity for operation</typeparam>
    public interface IBaseRepository<TEntity> 
    {
        /// <summary>
        ///     Save entity asynchronously
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <returns>Task</returns>
        Task SaveAsync(TEntity entity);

        /// <summary>
        ///     Asynchronously return a read-only collection of Entities for specified predicate
        /// </summary>
        /// <param name="predicate">Predicate that declare how to filter records.</param>
        /// <returns>Returns readonly collection of entities that was returned after filtration</returns>
        Task<IReadOnlyCollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        ///     Asynchronously return a read-only collection of Entities for specified predicate according
        ///     to specified projection.
        /// </summary>
        /// <param name="predicate">Predicate that declare how to filter records.</param>
        /// <param name="projection">Projection that will be returned</param>
        /// <typeparam name="TProjection">Result projection type</typeparam>
        /// <returns></returns>
        Task<IReadOnlyCollection<TProjection>> GetByAsync<TProjection>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProjection>> projection);
    }
}
