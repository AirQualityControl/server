using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IClientRepository : IBaseRepository
    {
        Task<ApiUser> FindClientOwner(string clientId);

        Task<ApiClient> GetById(string clientId);

        Task Update(ApiClient client);
        Task<ApiClient> GetClientByApiKey(ApiKey fromString);
    }
}