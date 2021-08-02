using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IApiUserRepository
    {
        /// <summary>
        ///     Fetch a whole entity from DB.
        /// </summary>
        /// <param name="id">Unique identifier(primary key) of entity</param>
        /// <returns>Return single entity in case if operation is success</returns>
        /// <throws>Throws UserNotFoundException in case if entity not found.</throws>
        Task<ApiUser> GetById(string id);
        
        /// <summary>
        ///     Try to find an entity in DB
        /// </summary>
        /// <param name="id">unique identifier(primary key) of entity</param>
        /// <returns>If entity was found by id fetched record returns, otherwise null object</returns>
        Task<ApiUser> FindById(string id);
        
        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);
        Task Update(ApiUser apiUser);
        Task<DeletionResult> DeleteById(string id);
    }

    public enum DeletionResult
    {
        Success,
        NotFound,
    }
}