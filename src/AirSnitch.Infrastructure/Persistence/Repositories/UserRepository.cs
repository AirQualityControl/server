using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Core.Domain.Exceptions;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Persistence;
using AirSnitch.Infrastructure.Persistence.StorageModels;



namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    /// <inheritdoc cref="IUserRepository" />
    internal class UserRepository : BaseRepository<UserStorageModel>, IUserRepository
    {
        protected override string CollectionName => "user";

        ///<inheritdoc/>
        public async Task Insert(User user)
        {
            Contract.Requires(user != null);

            var userStorageModel = UserStorageModel.CreateFromDomainModel(user);
            
            await Collection.InsertOneAsync(userStorageModel);
        }

        ///<inheritdoc/>
        public async Task Update(User user)
        {
            Contract.Requires(user != null);
            Contract.Requires(user.ClientUserId != default);

            var userStorageModel = UserStorageModel.CreateFromDomainModel(user);
            
            await UpdateByAsync(
                entity: userStorageModel, 
                entityMemberSelector: u => u.ClientUserId,
                memberValue: user.ClientUserId
            );
        }
        
        ///<inheritdoc/>
        public async Task<User> GetByIdAsync(long clientUserId)
        {
            Contract.Requires(clientUserId != default);

            var users =  await GetByAsync(
                u => u.ClientUserId == clientUserId
            );

            var userStorageModel = users.SingleOrDefault();
            
            if (userStorageModel != default(UserStorageModel))
            {
                return userStorageModel.MapToDomainModel();
            }
            throw new UserNotFoundException();
        }
        
        ///<inheritdoc/>
        public async Task<User> FindByIdAsync(long clientUserId)
        {
            Contract.Requires(clientUserId != default);

            var resultSequence = await GetByAsync(
                u => u.ClientUserId == clientUserId
            );

            var userStorageModel = resultSequence.SingleOrDefault();

            return userStorageModel == default(UserStorageModel) ? User.Empty : userStorageModel.MapToDomainModel();
        }
    }
}
