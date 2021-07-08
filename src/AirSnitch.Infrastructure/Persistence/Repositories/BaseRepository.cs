using System.Threading.Tasks;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme)
        {
            throw new System.NotImplementedException();
        }
    }
}