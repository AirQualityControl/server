using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    /// <summary>
    ///     Interface declare a set of operations that could be applicable for any storage model
    /// </summary>
    public interface IGenericRepository<TEntity>
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
        
        /// <summary>
        ///     Method that execute query from provided scheme and returns a result
        /// </summary>
        /// <param name="query">QueryScheme incoming scheme that will be converted to query and executed in db</param>
        /// <returns>An instance of QueryResult class</returns>
        Task<QueryResult> ExecuteQueryAsync(IQuery query);

        /// <summary>
        ///     Method deletes exactly one records by specified expression.
        ///     In case if there are more that one records matched exception will be thrown
        /// </summary>
        /// <param name="filter">Predicate that declare how to filter records.</param>
        /// <returns>Returns not null deletion result</returns>
        Task<DeletionResult> DeleteOneBy(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        ///     Method deletes all records by specified expression.
        ///     In case of success deletion result wil be returned.
        /// </summary>
        /// <param name="filter">Predicate that declare what kind of records should be deleted</param>
        /// <returns></returns>
        Task<DeletionResult> DeleteBy(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        ///     Returns a number of records
        /// </summary>
        Task<long> Count { get; }
    }
}