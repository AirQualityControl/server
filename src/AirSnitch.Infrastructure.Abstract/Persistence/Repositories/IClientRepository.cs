using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IClientRepository
    {
        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);
        Task<ApiUser> FindClientOwner(string clientId);
    }
}