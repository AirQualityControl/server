using System.Threading.Tasks;
using AirSnitch.Api.Rest;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.MonitoringStation;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers.MonitoringStationController
{
    /// <summary>
    /// Controller that represent a MonitoringStation resource
    /// </summary>
    [ApiController]
    //[Authorize(AuthenticationSchemes = Constants.Authentication.Scheme.ApiKey)]
    [Route("monitoringStation")]
    public class MonitoringStationController : RestApiController
    {
        private readonly IMonitoringStationRepository _monitoringStationRepository;
        
        public MonitoringStationController(
            DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, 
            IApiResourceRegistry apiResourceRegistry, IMonitoringStationRepository monitoringStationRepository) : base(apiResourcesGraph, apiResourceRegistry)
        {
            _monitoringStationRepository = monitoringStationRepository;
        }
        
        protected override IApiResourceMetaInfo CurrentResource => new MonitoringStationResource();

        /// <summary>
        ///     Returns all active monitoring station available in the system
        /// </summary>
        /// <url>http://apiurl/apiUsers</url>
        /// <param name="requestParameters">Requested parameter's that will be send alongside with a request.</param>
        /// <returns>Returns a rest api result that contains fetched data</returns>
        /// <response code="200">Returns 200 when everything is correct</response>
        /// <response code="400">If request parameters has an invalid state</response>   
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery]RequestParameters requestParameters)
        {
            var queryScheme = GenerateQueryScheme(requestParameters.IncludesString, requestParameters.PageOptions);

            QueryResult result = await _monitoringStationRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            
            return new RestApiResult(
                new RestResponseBody(
                    Request,
                    result,
                    RelatedResources,
                    GetIncludedResources(requestParameters.IncludesString)
                )
            );
        }
        
        /// <summary>
        ///     Returns a requested monitoring station by Id
        /// </summary>
        /// <url>http://apiurl/apiUser/Id</url>
        /// <param name="id">Valid identifier of monitoring station</param>
        /// <param name="includedResources">A collection of included resources the will be queried in single request alongside with a main resource(MonitoringStation)</param>>
        /// <returns>Existing apiUser</returns>
        /// <response code="200">Returns 200 when everything is correct</response>
        /// <response code="400">If request parameters has an invalid state</response>
        /// <response code="404">Returns 404 when requested resource was not found</response> 
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id, string includedResources)
        {
            var queryScheme = GenerateQueryScheme(includedResources);
            
            queryScheme.AddColumnFilter(
                new EqualColumnFilter(
                     column: CurrentResource.QueryColumn,
                     value:id
                    )
                );
            
            QueryResult result = await _monitoringStationRepository.ExecuteQueryFromSchemeAsync(queryScheme);

            return new RestApiResult(
                new RestResponseBody(
                    Request,
                    result,
                    RelatedResources,
                    base.GetIncludedResources(includedResources)//TODO: not from base
                )
            );
        }
        
        /// <summary>
        ///     Returns a requested monitoring station by Id
        /// </summary>
        /// <url>http://apiurl/apiUser/Id</url>
        /// <param name="id">Valid identifier of monitoring station</param>
        /// <returns>Existing apiUser</returns>
        /// <response code="200">Returns 200 when everything is correct</response>
        /// <response code="400">If request parameters has an invalid state</response>
        /// <response code="404">Returns 404 when requested resource was not found</response> 
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpGet]
        [Route("{id}/airPollution")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAirPollution(string id)
        {
            var station = await _monitoringStationRepository.GetByIdAsync(id);

            if (station.IsEmpty)
            {
                return NotFound();
            }

            var airPollution = station.GetAirPollution();

            var stationAirPollutionViewModel = new StationAirPollutionViewModel(airPollution);
            
            return new RestApiResult(
                new RestResponseBody(
                    Request,
                    stationAirPollutionViewModel.GetResult(),
                    RelatedResources
                )
            );
        }
        
        /// <summary>
        ///     Returns a requested monitoring station by Id
        /// </summary>
        /// <url>http://apiurl/apiUser/Id</url>
        /// <param name="id">Valid identifier of monitoring station</param>
        /// <returns>Existing apiUser</returns>
        /// <response code="200">Returns 200 when everything is correct</response>
        /// <response code="400">If request parameters has an invalid state</response>
        /// <response code="404">Returns 404 when requested resource was not found</response> 
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpGet]
        [Route("{id}/airQualityIndex")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAirQualityIndex(string id)
        {
            var station = await _monitoringStationRepository.GetByIdAsync(id);

            if (station.IsEmpty)
            {
                return NotFound();
            }

            var airPollution = station.GetAirPollution();

            var usaAqi = new UsaAirQualityIndex();
            
            var airQualityIndexValue = usaAqi.Calculate(airPollution);

            var stationAirQualityIndexViewModel = new StationAirQualityIndexViewModel(usaAqi, airQualityIndexValue);
            
            return new RestApiResult(
                new RestResponseBody(
                    Request,
                    stationAirQualityIndexViewModel.GetResult(),
                    RelatedResources
                )
            );
        }
        
        /// <summary>
        ///     Returns a requested monitoring station by Id
        /// </summary>
        /// <url>http://apiurl/apiUser/Id</url>
        /// <param name="id">Valid identifier of monitoring station</param>
        /// <returns>Existing apiUser</returns>
        /// <response code="200">Returns 200 when everything is correct</response>
        /// <response code="400">If request parameters has an invalid state</response>
        /// <response code="404">Returns 404 when requested resource was not found</response> 
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpGet]
        [Route("{id}/dataProvider")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDataProvider(string id)
        {
            var station = await _monitoringStationRepository.GetByIdAsync(id);

            if (station.IsEmpty)
            {
                return NotFound();
            }

            var stationOwner = station.GetOwner();

            var dataProviderViewModel = new StationDataProviderViewModel(stationOwner);
            
            return new RestApiResult(
                new RestResponseBody(
                    Request,
                    dataProviderViewModel.GetResult(),
                    RelatedResources
                )
            );
        }
    }
}