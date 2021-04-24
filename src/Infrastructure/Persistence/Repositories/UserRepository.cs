using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Core.Domain.Exceptions;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Persistence;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    /// <inheritdoc cref="IUserRepository" />
    internal class UserRepository : BaseRepository<UserStorageModel>, IUserRepository
    {
        protected override string CollectionName => "user";

        ///<inheritdoc/>
        public async Task Insert(User user)
        {
            Require.That(user, Is.NotNull);

            var userStorageModel = UserStorageModel.CreateFromDomainModel(user);
            
            await Collection.InsertOneAsync(userStorageModel);
        }

        ///<inheritdoc/>
        public async Task Update(User user)
        {
            Require.That(user, Is.NotNull);
            Require.That(user.ClientUserId, Is.NotDefault);

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
            Require.That(clientUserId, Is.NotDefault);

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
            Require.That(clientUserId, Is.NotDefault);

            var resultSequence = await GetByAsync(
                u => u.ClientUserId == clientUserId
            );

            var userStorageModel = resultSequence.SingleOrDefault();

            return userStorageModel == default(UserStorageModel) ? User.Empty : userStorageModel.MapToDomainModel();
        }
    }
}
