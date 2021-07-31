using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IApiUserRepository
    {
        Task<ApiUser> FindById(string id);

        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);
        Task Update(ApiUser apiUser);
        Task<DeletionResult> DeleteById(string id);
    }

    public enum DeletionResult
    {
        Success,
        NotFound,
    }
}