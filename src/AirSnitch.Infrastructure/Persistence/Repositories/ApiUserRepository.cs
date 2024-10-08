using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.Repositories.Common;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class ApiUserRepository : IApiUserRepository
    {
        private readonly IGenericRepository<ApiUserStorageModel> _genericRepository;

        public ApiUserRepository(MongoDbClient client)
        {
            _genericRepository = new MongoDbGenericRepository<ApiUserStorageModel>(client, "apiUser");
        }
        
        public async Task Add(ApiUser apiUser)
        {
            var apiUserStorageModel = ApiUserStorageModel.CreateFromDomainModel(apiUser);
            
            await _genericRepository.SaveAsync(apiUserStorageModel);
        }
        
        public async Task<ApiUser> GetById(string id)
        {
            Guid.Parse(id);
            
            var users =  await _genericRepository.GetByAsync(
                u => u.Id == id
            );

            var apiUserStorageModel = users.SingleOrDefault();
            
            if (apiUserStorageModel != default(ApiUserStorageModel))
            {
                return apiUserStorageModel.MapToDomainModel();
            }
            throw new ItemNotFoundException();
        }

        public async Task<ApiUser> FindById(string id)
        {
            Guid.Parse(id);
            
            var resultSequence = await _genericRepository.GetByAsync(m => m.Id == id);

            var userStorageModel = resultSequence.SingleOrDefault();

            return userStorageModel == default(ApiUserStorageModel) ? ApiUser.Empty : userStorageModel.MapToDomainModel();
        }

        public async Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            var query = MongoDbQuery.CreateFromScheme(queryScheme);

            var queryResultTask = _genericRepository.ExecuteQueryAsync(query);
            var totalNumberOfDocumentsTask = _genericRepository.Count;
            
            await Task.WhenAll(queryResultTask, totalNumberOfDocumentsTask);

            return new QueryResult(
                queryResultTask.Result,
                new MongoDbQueryResultFormatter(),
                new PageOptions(
                    pageNumber: queryScheme.PageOptions.PageNumber, 
                    totalNumberOfItems: totalNumberOfDocumentsTask.Result, 
                    itemsPerPage:queryScheme.PageOptions.ItemsLimit)
                );
        }

        public async Task Update(ApiUser apiUser)
        {
            Require.That(apiUser, Is.NotNull);
            
            var userStorageModel = ApiUserStorageModel.CreateFromDomainModel(apiUser);

            await _genericRepository.UpdateByAsync(
                entity:userStorageModel, 
                entityMemberSelector:u => u.Id, 
                memberValue:userStorageModel.Id
            );
        }

        public Task<DeletionResult> Delete(string id)
        {
            Guid.Parse(id);
            
            Expression<Func<ApiUserStorageModel, bool>> deleteCondition = 
                apiUserStorageModel => apiUserStorageModel.Id == id; 
            
            return _genericRepository
                .DeleteOneBy(deleteCondition);
        }

        public async Task<bool> IsUserAlreadyExists(ApiUser apiUser)
        {
            var apiUserEmail = apiUser.Profile.GetEmailValue();
            
            var user = await _genericRepository.GetByAsync(
                predicate:usrStorageModel => usrStorageModel.Email.Equals(apiUserEmail),
                projection:u => u.PrimaryKey
            );

            return user.Any();
        }
    }
}