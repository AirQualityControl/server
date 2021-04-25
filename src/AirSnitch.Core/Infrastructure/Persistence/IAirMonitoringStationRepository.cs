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

        /// <summary>
        ///     Returns a page with a certain number of elements inside.
        /// </summary>
        /// <param name="pageOffset">Offset of the requested page.By default it is 0.
        /// It means that no item will be skipped and result will be return from the beginning of collection</param>
        /// <param name="numberOfItems">Number of items that will be returned inside of the page after successful queery execution.
        /// Default value is 50.Max value that could be returned is 100.</param>
        /// <returns>Returns a page that contains all necessary data.</returns>
        public Task<Page<AirMonitoringStation>> GetPage(int pageOffset, int numberOfItems = 50);
    }
}
