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

        public ApiUserRepository(IGenericRepository<ApiUserStorageModel> genericRepository)
        {
            _genericRepository = genericRepository;
            _genericRepository.SetCollectionName("apiUser");
        }
        
        public async Task<ApiUser> FindById(string id)
        {
            var storageModel = await _genericRepository.FindByIdAsync(id);
            return null;
        }

        public Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            var result = _genericRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            return result;
        }
    }
}