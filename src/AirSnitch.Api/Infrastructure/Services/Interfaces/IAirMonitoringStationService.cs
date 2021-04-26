using AirSnitch.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public interface IAirMonitoringStationService
    {
        Task<AirMonitoringStationDTO> GetByIdAsync(string stationId);
        Task<AirPollutionDTO> GetIncludedAirpolution(string stationId);
        Task<DataProviderDTO> GetIncludedDataProvider(string stationId);
        Task<CityDTO> GetIncludedCity(string stationId);
        Task<(Dictionary<string, AirMonitoringStationDTO>, int)> GetPaginated(int limit, int offset);
    }
}