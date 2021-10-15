using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Cryptography;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Cryptography.Hashing;
using AirSnitch.Infrastructure.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.Repositories.Common;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IApiKeyHashAlgorithm _apiKeyHashAlgorithm;
        private readonly IGenericRepository<ApiUser> _genericRepository;
        private readonly IMongoCollection<ApiUserStorageModel> _apiUserCollection;
        public ClientRepository(MongoDbClient client, IApiKeyHashAlgorithm apiKeyHashAlgorithm)
        {
            _apiKeyHashAlgorithm = apiKeyHashAlgorithm;
            _apiUserCollection = client.Db.GetCollection<ApiUserStorageModel>("apiUser");
            _genericRepository = new MongoDbGenericRepository<ApiUser>(client, "apiUser");
        }
        
        //TODO: move to base class to avoid duplication
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

        public async Task<ApiClient> GetById(string clientId)
        {
            var filter = Builders<ApiUserStorageModel>.Filter.ElemMatch(x => x.Clients, c => c.Id == clientId);
            var clients = await _apiUserCollection.Find(filter).Project(c => c.Clients).ToListAsync();
            var client = clients.Single().Single(c => c.Id == clientId);

            return ClientStorageModel.BuildFromStorageModel(client);
        }

        public async Task Update(ApiClient client)
        {
            var clientStorageModel = ClientStorageModel.BuildFromDomainModel(client);

            clientStorageModel.SetApiKeyValue(_apiKeyHashAlgorithm.GetHash(client.ApiKey));
            
            var update = Builders<ApiUserStorageModel>.Update.Combine(
                Builders<ApiUserStorageModel>.Update.Set(p => p.Clients[0].Name, clientStorageModel.Name),
                Builders<ApiUserStorageModel>.Update.Set(p => p.Clients[0].Description, clientStorageModel.Description),
                Builders<ApiUserStorageModel>.Update.Set(p => p.Clients[0].Type, clientStorageModel.Type),
                Builders<ApiUserStorageModel>.Update.Set(p => p.Clients[0].CreatedOn, clientStorageModel.CreatedOn),
                Builders<ApiUserStorageModel>.Update.Set(p => p.Clients[0].ApiKey, clientStorageModel.ApiKey)
            );

            var filter = Builders<ApiUserStorageModel>.Filter.And(
                Builders<ApiUserStorageModel>.Filter.ElemMatch(
                    x => x.Clients, c => c.Id == clientStorageModel.Id));

            await _apiUserCollection.UpdateOneAsync(filter, update);
        }

        public async Task<ApiClient> GetClientByApiKey(ApiKey apiKey)
        {
            string apiKeyHash = Pbkdf2Hash.Generate(apiKey.Value);
            
            var filter = Builders<ApiUserStorageModel>.Filter.ElemMatch(
                    x => x.Clients, c => c.ApiKey.Value == apiKeyHash);
            
            var clients = await _apiUserCollection.Find(filter).Project(c => c.Clients).ToListAsync();

            if (clients.Any())
            {
                var client = clients.Single().Single(c => c.ApiKey.Value == apiKeyHash);
                return ClientStorageModel.BuildFromStorageModel(client);
            }
            return ApiClient.Empty;
        }
    }
}