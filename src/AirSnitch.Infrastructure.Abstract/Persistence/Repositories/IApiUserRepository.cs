using System.Threading.Tasks;
using AirSnitch.Domain.Models;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IApiUserRepository
    {
        Task<ApiUser> FindById(string id);

        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);
    }
}