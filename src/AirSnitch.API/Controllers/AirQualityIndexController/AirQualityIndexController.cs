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
        private readonly IAirPollutionRepository _airPollutionRepository;
        
        public AirQualityIndexController(
            IAirPollutionRepository airPollutionRepository,
            DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, 
            IApiResourceRegistry apiResourceRegistry) : base(apiResourcesGraph, apiResourceRegistry)
        {
            _airPollutionRepository = airPollutionRepository;
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
            var airPollution = await _airPollutionRepository.GetLatestAirPollutionByGeolocation(requestParams.Geolocation);

            var usaAqi = new UsaAirQualityIndex();
            
            IAirQualityIndexValue indexValue = usaAqi.Calculate(airPollution);
            
            var aqiViewModel = new AirQualityIndexViewModel(index: usaAqi,  indexValue:indexValue);
            
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