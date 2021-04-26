using AirSnitch.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public interface ICityService
    {
        Task<CityDTO> GetByIdAsync(string stationId);
        Task<(Dictionary<string, CityDTO>, int)> GetPaginated(int limit, int offset);
    }
}