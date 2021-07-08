using System.Threading.Tasks;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IBaseRepository
    {
        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);
    }
}