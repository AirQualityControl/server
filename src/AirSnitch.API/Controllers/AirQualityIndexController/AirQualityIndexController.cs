using System;
using System.Linq;
using System.Threading.Tasks;
using AirSnitch.Api.Controllers.AirQualityIndexController.ViewModel;
using AirSnitch.Api.Rest;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.AirQualityIndex;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers.AirQualityIndexController
{
    /// <summary>
    /// Controller that represent a ApiUser resource
    /// </summary>
    [ApiController]
    [ApiVersion( "1" )]
    [Authorize(AuthenticationSchemes = Constants.Authentication.Scheme.ApiKey)]
    [Route( "v{version:apiVersion}/airQualityIndex" )]
    public class AirQualityIndexController : RestApiController
    {
        private readonly IMonitoringStationRepository _monitoringStationRepository;
        
        public AirQualityIndexController(
            DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, 
            IMonitoringStationRepository repository,
            IApiResourceRegistry apiResourceRegistry) : base(apiResourcesGraph, apiResourceRegistry)
        {
            _monitoringStationRepository = repository;
        }

        protected override IApiResourceMetaInfo CurrentResource => new AirQualityIndexResource();

        /// <summary>
        ///     Returns an air quality index value for requested location
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIndexValue([FromQuery]AirQualityIndexRequestParams requestParams)
        {
            var nearestStations =
                await _monitoringStationRepository.GetNearestStations(requestParams.Geolocation, numberOfStations:10);

            if (!nearestStations.Any())
            {
                return NotFound();
            }
            
            var activeStation = nearestStations.FirstOrDefault(station => station.HasActualData());
            
            if (activeStation == null)
            {
                return NotFound();
            }
            
            var airPollution = activeStation.GetAirPollution();

            var index = airPollution.GetAirQualityIndexValue();

            if (index == null)
            {
                throw new Exception("Index needs to be calculated");
            }

            var aqiViewModel = new AirQualityIndexViewModel(
                index: new UsaAirQualityIndex(),
                indexValue:index,
                Request);
            
            aqiViewModel.SetMeasurementDateTime(airPollution.GetMeasurementsDateTime());
            
            aqiViewModel.SetStationId(activeStation.Id);
            
            return new RestApiResult(
                new RestResponseBody(
                    Request, 
                    aqiViewModel.GetResult(),
                    RelatedResources
                )
            );
        }
    }
}