using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Persistence.StorageModels;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class ApiUserRepository : IApiUserRepository
    {
        private readonly IGenericRepository<ApiUserStorageModel> _genericRepository;

        public ApiUserRepository(MongoDbClient client)
        {
            _genericRepository = new MongoDbGenericRepository<ApiUserStorageModel>(client, "apiUser");
        }
        
        public async Task<ApiUser> FindById(string id)
        {
            var storageModel = await _genericRepository.FindByIdAsync(id);
            return null;
        }

        public async Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            var query = MongoDbQuery.CreateFromScheme(queryScheme);

            var result = await _genericRepository.ExecuteQueryAsync(query);
            return result;
        }
    }
}