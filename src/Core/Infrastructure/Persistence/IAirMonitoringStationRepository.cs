using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;

namespace AirSnitch.Core.Infrastructure.Persistence
{
    /// <summary>
    ///     Interface that declare a set of operations specific for AirMonitoringStation entity
    /// </summary>
    public interface IAirMonitoringStationRepository
    {
        /// <summary>
        ///     Returns single airMonitoringStation by supplied Id
        /// </summary>
        /// <param name="id">Unique identifier of monitoring station</param>
        /// <returns>Instance of airMonitoringStation</returns>
        Task<AirMonitoringStation> GetByIdAsync(string id);

        /// <summary>
        ///     Returns ICollection of AirMonitoringStations for specified geolocation
        /// </summary>
        /// <param name="geoLocation">Geolocation coordinates</param>
        /// <param name="numberOfNearestStations">Number of nearest stations to return</param>
        /// <returns></returns>
        Task<IReadOnlyCollection<AirMonitoringStation>> GetTopNearestStationsAsync(GeoLocation geoLocation, int numberOfNearestStations);
    }
}
