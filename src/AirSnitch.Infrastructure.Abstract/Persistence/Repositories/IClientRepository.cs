using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IClientRepository : IBaseRepository
    {
        Task<ApiUser> FindClientOwner(string clientId);
        Task<ApiClient> FindById(string id);
        Task Update(ApiClient existingClient);
    }
}