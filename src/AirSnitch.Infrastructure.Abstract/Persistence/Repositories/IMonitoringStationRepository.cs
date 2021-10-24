using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IMonitoringStationRepository
    {
        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<MonitoringStation> FindByIdAsync(string id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MonitoringStation> GetByIdAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<MonitoringStation> GetNearestStation(GeoCoordinates geoCoordinates, int radius = default);
    }
}