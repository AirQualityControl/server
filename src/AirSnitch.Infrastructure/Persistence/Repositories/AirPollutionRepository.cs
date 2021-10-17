using System.Threading.Tasks;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    public class AirPollutionRepository : IAirPollutionRepository
    {
        public Task<AirPollution> GetLatestAirPollutionByGeolocation(GeoLocation geoLocation)
        {
            return Task.FromResult(new AirPollution());
        }
    }
}