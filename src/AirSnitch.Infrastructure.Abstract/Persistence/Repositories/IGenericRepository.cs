using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    /// <summary>
    ///     Interface declare a set of operations that could be applicable for any storage model
    /// </summary>
    public interface IGenericRepository<TEntity>
    {

        void SetCollectionName(string collectionName);
        
        /// <summary>
        ///     Try to find an entity in DB
        /// </summary>
        /// <param name="id">unique identifier(primary key) of entity</param>
        /// <returns>If entity was found by id fetched record returns, otherwise null object</returns>
        Task<TEntity> FindByIdAsync(string id);
        
        /// <summary>
        ///     Fetch a whole entity from DB.
        /// </summary>
        /// <param name="id">Unique identifier(primary key) of entity</param>
        /// <returns>Return single entity in case if operation is success</returns>
        /// <throws>Throws UserNotFoundException in case if entity not found.</throws>
        Task<TEntity> GetById(string id);
        
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
        /// <param name="queryScheme">QueryScheme incoming scheme that will be converted to query and executed in db</param>
        /// <returns>An instance of QueryResult class</returns>
        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);
    }
}