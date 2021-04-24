using AirSnitch.Core.Domain.Models;

namespace AirSnitch.Core.UseCases.GetAirPollution
{
    public interface IGetAirPollutionByGeoLocationUseCase : IScalarUseCase<AirPollution, GeoLocation>
    {
        
    }
}