using System.Collections.Generic;
using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IMonitoringStationRepository
    {
        Task<QueryResult> ExecuteQueryFromSchemeAsync(QueryScheme queryScheme);

        /// <summary>
        ///     Try to find station by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<MonitoringStation> FindByIdAsync(string id);
        
        /// <summary>
        ///     Return AirMonitoringStation by specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MonitoringStation> GetByIdAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<MonitoringStation> GetNearestStation(GeoCoordinates geoCoordinates, int radius = 5, int numberOfStations = 10);

        /// <summary>
        ///     Search station by provider station name
        /// </summary>
        /// <param name="providerStationName"></param>
        /// <returns></returns>
        Task<MonitoringStation> FindByProviderNameAsync(string providerStationName);

        /// <summary>
        /// Returns n nearest stations by geolocation
        /// </summary>
        /// <param name="geoCoordinates"></param>
        /// <param name="numberOfStations"></param>
        /// <returns></returns>
        Task<ICollection<MonitoringStation>> GetNearestStations(GeoCoordinates geoCoordinates,
            int numberOfStations = 10);
        
        Task AddAsync(MonitoringStation monitoringStation);
        Task UpdateAsync(MonitoringStation monitoringStation);
    }
}