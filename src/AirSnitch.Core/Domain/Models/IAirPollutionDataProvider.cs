using System.Threading;
using System.Threading.Tasks;

namespace AirSnitch.Core.Domain.Models
{
    public interface IAirPollutionDataProvider
    {
        /// <summary>
        /// Unique tag of data provider
        /// </summary>
        public AirPollutionDataProviderTag Tag { get; }
        
        /// <summary>
        /// Returns latest air pollution value.
        /// </summary>
        /// <returns></returns>
        Task<AirPollution> GetLatestDataAsync(AirMonitoringStation station);
    }
}