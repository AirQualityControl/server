using AirSnitch.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetByIdAsync(string stationId);
        Task<AirMonitoringStationDTO> GetIncludedAirMonitoringStation(string userId);
        Task<AirPollutionDTO> GetIncludedAirpolution(string userId);
        Task<CityDTO> GetIncludedCity(string userId);
        Task<DataProviderDTO> GetIncludedDataProvider(string userId);
        Task<(Dictionary<string, UserDTO>, int)> GetPaginated(int limit, int offset);
        Task<Dictionary<string, object>> GetIncludes(string[] includes, string userId);
    }
}