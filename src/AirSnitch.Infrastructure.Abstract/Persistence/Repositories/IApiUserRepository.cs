using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.StorageModels;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IApiUserRepository : IBaseRepository
    {
        Task<ApiUser> FindById(string id);
    }
}