using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Bson;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class ApiUserRepository : IApiUserRepository
    {
        private readonly IGenericRepository<ApiUserStorageModel> _genericRepository;

        public ApiUserRepository(MongoDbClient client)
        {
            _genericRepository = new MongoDbGenericRepository<ApiUserStorageModel>(client, "apiUser");
        }
        
        public async Task Save(ApiUser apiUser)
        {
            var apiUserStorageModel = ApiUserStorageModel.CreateFromDomainModel(apiUser);
            
            await _genericRepository.SaveAsync(apiUserStorageModel);
        }
        
        public async Task<ApiUser> GetById(string id)
        {
            var users =  await _genericRepository.GetByAsync(
                u => u.Id == ObjectId.Parse(id)
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
            var resultSequence = await _genericRepository.GetByAsync(m => m.Id == ObjectId.Parse(id));

            var userStorageModel = resultSequence.SingleOrDefault();

            return userStorageModel == default(ApiUserStorageModel) ? ApiUser.Empty : userStorageModel.MapToDomainModel();
        }

        public async Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            var query = MongoDbQuery.CreateFromScheme(queryScheme);

            var queryResultTask = _genericRepository.ExecuteQueryAsync(query);
            var totalNumberOfDocumentsTask = _genericRepository.Count;
            
            await Task.WhenAll(queryResultTask, totalNumberOfDocumentsTask);

            return new QueryResult(queryResultTask.Result.Value, 
                new PageOptions(
                    pageNumber: queryScheme.PageOptions.PageNumber, 
                    totalNumberOfItems: totalNumberOfDocumentsTask.Result, 
                    itemsPerPage:queryScheme.PageOptions.ItemsLimit));
        }

        public Task Update(ApiUser apiUser)
        {
            return Task.CompletedTask;
        }

        public Task<DeletionResult> DeleteById(string id)
        {
            Expression<Func<ApiUserStorageModel, bool>> deleteCondition = 
                apiUserStorageModel => apiUserStorageModel.Id == ObjectId.Parse(id); 
            
            return _genericRepository
                .DeleteOneBy(deleteCondition);
        }
    }
}