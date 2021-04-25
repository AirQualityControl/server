using System.Threading.Tasks;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure.Logging;
using AirSnitch.Core.Infrastructure.Persistence;

namespace AirSnitch.Core.UseCases.GetAirPollution
{
    public class GetAirPollutionByGeoLocationUseCase : IGetAirPollutionByGeoLocationUseCase
    {
        private readonly IAirMonitoringStationRepository _airMonitoringStationRepository;
        private readonly ILog _logger;

        public GetAirPollutionByGeoLocationUseCase(
            IAirMonitoringStationRepository airMonitoringStationRepository, ILog logger)
        {
            _airMonitoringStationRepository = airMonitoringStationRepository;
            _logger = logger;
        }
        
        public async Task<GenericUseCaseExecutionResult<AirPollution>> ExecuteAsync(GeoLocation geoLocation)
        {
            AirPollution airPollutionResult = null;
            
            var nearestStations = 
                await _airMonitoringStationRepository.GetTopNearestStationsAsync(geoLocation, 5);

            foreach (var station in nearestStations)
            {
                var airPollution = await station.GetLatestAirPollutionAsync();
                if (!airPollution.IsEmpty)
                {
                    airPollutionResult = airPollution;
                    break;
                }
                _logger.Warn($"There are no data for monitoring station {station} right now.");
            }

            return await Task.FromResult(
                BuildUseCaseResult(airPollutionResult)
            );
        }

        private GenericUseCaseExecutionResult<AirPollution> BuildUseCaseResult(AirPollution airPollution)
        {
            if (airPollution == null)
            {
                _logger.Error("There are no station with actual data to satisfy user request!!");
                return new GenericUseCaseExecutionResult<AirPollution>()
                {
                    IsSuccess = false,
                };
            }

            return new GenericUseCaseExecutionResult<AirPollution>()
            {
                IsSuccess = true,
                Result = airPollution
            };
        }
    }
}