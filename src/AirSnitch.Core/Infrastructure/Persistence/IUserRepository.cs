using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;

namespace AirSnitch.Core.Infrastructure.Persistence
{
    /// <summary>
    ///     Interface declare a set of operations specific fot User entity
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        ///     Insert a new user to store
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task Insert(User user);

        /// <summary>
        ///     Update a whole user entity
        /// </summary>
        /// <param name="user">User entity updated state</param>
        /// <returns>Task</returns>
        Task Update(User user);

        /// <summary>
        ///     Fetch a whole user entity from DB.
        /// </summary>
        /// <param name="clientUserId">Unique identifier of user in client's system</param>
        /// <returns>Return single user in case if operation is success</returns>
        /// <throws>Throws UserNotFoundException in case if user not found.</throws>
        Task<User> GetByIdAsync(long clientUserId);

        /// <summary>
        ///     Try to find a user in DB
        /// </summary>
        /// <param name="clientUserId">Unique identifier of user in client's system</param>
        /// <returns>If user was found by id fetched record returns, otherwise null object </returns>
        Task<User> FindByIdAsync(long clientUserId);
    }
}
