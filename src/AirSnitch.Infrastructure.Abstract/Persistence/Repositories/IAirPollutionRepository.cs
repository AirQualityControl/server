using System.Threading.Tasks;
using AirSnitch.Domain.Models;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Repositories
{
    public interface IAirPollutionRepository
    {
        Task<AirPollution> GetLatestAirPollutionByGeolocation(GeoCoordinates geoCoordinates);
    }
}