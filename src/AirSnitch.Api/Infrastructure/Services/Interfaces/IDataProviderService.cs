using AirSnitch.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.Services
{
    public interface IDataProviderService
    {
        Task<DataProviderDTO> GetByIdAsync(string stationId);
        Task<(Dictionary<string, DataProviderDTO>, int)> GetPaginated(int limit, int offset);
    }
}