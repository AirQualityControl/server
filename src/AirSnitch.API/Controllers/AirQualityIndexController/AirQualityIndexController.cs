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
    [Authorize(AuthenticationSchemes = Constants.Authentication.Scheme.ApiKey)]
    [Route("airQualityIndex")]
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
            //TODO:https://github.com/AirQualityControl/server/issues/93
            var nearestStation =
                await _monitoringStationRepository.GetNearestStation(requestParams.Geolocation, requestParams.Radius);

            if (nearestStation.IsEmpty)
            {
                return NotFound();
            }

            var airPollution = nearestStation.GetAirPollution();

            var usaAqi = new UsaAirQualityIndex();

            var indexValue = usaAqi.Calculate(airPollution);
            
            var aqiViewModel = new AirQualityIndexViewModel(
                index: usaAqi,
                indexValue:indexValue,
                Request);

            
            //aqiViewModel.SetMeasurementDateTime(airPollution.GetMeasurementsDateTime());
            
            aqiViewModel.SetStationId("5480d666-cd1e-45ff-9e63-f283592c72e2");

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