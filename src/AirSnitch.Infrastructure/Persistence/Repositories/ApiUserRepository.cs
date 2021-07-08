using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using AirSnitch.Infrastructure.Abstract.Persistence.StorageModels;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class ApiUserRepository : BaseRepository, IApiUserRepository
    {
        private readonly IGenericRepository<ApiUserStorageModel> _genericRepository;
        
        public ApiUserRepository(IGenericRepository<ApiUserStorageModel> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        
        public async Task<ApiUser> FindById(string id)
        {
            var storageModel = await _genericRepository.FindByIdAsync(id);
            return null;
        }
    }
}