using System;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IGenericRepository<ApiUser> _genericRepository;
        private readonly IMongoCollection<ApiUserStorageModel> _apiUserCollection;
        public ClientRepository(MongoDbClient client)
        {
            _apiUserCollection = client.Db.GetCollection<ApiUserStorageModel>("apiUser");
            _genericRepository = new MongoDbGenericRepository<ApiUser>(client, "apiUser");
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

        public async Task<ApiUser> FindClientOwner(string clientId)
        {
            var filter = Builders<ApiUserStorageModel>.Filter.ElemMatch(x => x.Clients, c => c.Id == clientId);
            var owners = await _apiUserCollection.Find(filter).ToListAsync();
            var ownerStorageModel = owners.FirstOrDefault();

            return ownerStorageModel == null ? ApiUser.Empty : ownerStorageModel.MapToDomainModel();
        }

        public async Task<ApiClient> FindById(string id)
        {
            Guid.Parse(id);

            var filter = Builders<ApiUserStorageModel>.Filter.ElemMatch(x => x.Clients, c => c.Id == id);
            var owners = await _apiUserCollection.Find(filter).Project(apiUser => apiUser.Clients).ToListAsync();
            var clientsStorageModel = owners.SingleOrDefault();

            if (clientsStorageModel != null)
            {
                var targetClient = clientsStorageModel.Single(c => c.Id == id);
                return ClientStorageModel.BuildFromStorageModel(targetClient);
            }

            return ApiClient.Empty;
        }

        public Task Update(ApiClient existingClient)
        {
            throw new System.NotImplementedException();
        }
    }
}