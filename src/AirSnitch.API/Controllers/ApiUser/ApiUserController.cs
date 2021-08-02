using System.Threading.Tasks;
using AirSnitch.Api.Rest;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.ApiUser;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers.ApiUser
{
    /// <summary>
    /// Controller that represent a ApiUser resource
    /// </summary>
    [ApiController]
    [Route("apiUser")]
    public class ApiUserController : RestApiController
    {
        private static readonly IApiResourceMetaInfo ApiUserResource = new ApiUserResource();
        private readonly IApiUserRepository _apiUserRepository;
        
        protected override IApiResourceMetaInfo CurrentResource => ApiUserResource;
        
        public ApiUserController(
            DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph,
            IApiResourceRegistry apiResourceRegistry,
            IApiUserRepository apiUserRepository) : base(apiResourcesGraph, apiResourceRegistry)
        {
            _apiUserRepository = apiUserRepository;
        }
        
        
        /// <summary>
        /// Returns all available api users.
        /// </summary>
        /// <url>http://apiurl/apiUsers</url>
        /// <param name="requestParameters">Requested parameter's that will be send alongside with a request.</param>
        /// <returns>Existing appointment data in an Appointment object or a business error.</returns>
        /// <response code="200">Returns 200 when everything is correct</response>
        /// <response code="400">If request parameters has an invalid state</response>   
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery]RequestParameters requestParameters)
        {
            var queryScheme = GenerateQueryScheme(requestParameters.Includes, requestParameters.PageOptions);

            QueryResult result = await _apiUserRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            
            return new RestApiResult(
                new RestResponseBody(
                    Request,
                    result,
                    RelatedResources
                )
            );
        }
        
        /// <summary>
        /// Returns a requested api user by Id
        /// </summary>
        /// <url>http://apiurl/apiUser/Id</url>
        /// <param name="id">Identifier of api user</param>
        /// <param name="includedResources">A collection of included resources the will be queried in single request alongside with a main resource(ApiUser)</param>>
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
            
            QueryResult result = await _apiUserRepository.ExecuteQueryFromSchemeAsync(queryScheme);

            return new RestApiResult(
                new RestResponseBody(
                    Request,
                    result,
                    RelatedResources
                )
            );
        }
        
        /// <summary>
        /// Delete an api user and all depended information by Id.
        /// </summary>
        /// <url>http://apiurl/apiUser/Id</url>
        /// <param name="id">Identifier of api user</param>
        /// <response code="200">Returns 204 when api user was successfully deleted.</response>
        /// <response code="404">If record to delete was not found</response>   
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteApiUserById(string id)
        {
            var deletionResult = await _apiUserRepository.DeleteById(id);

            return deletionResult == DeletionResult.Success ? NoContent() : NotFound();
        }
        
        /// <summary>
        /// Returns only clients for specific ApiUser
        /// </summary>
        /// <url>http://apiurl/apiUser/Id/clients</url>
        /// <param name="id">Identifier of api user</param>
        /// <response code="200">Returns 200 when data was successfully fetched</response>
        /// <response code="404">If parent record to fetch(ApiUser) was not found</response>   
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpGet]
        [Route("{id}/clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClients(string id)
        {
            var apiUser = await _apiUserRepository.FindById(id);

            return new RestApiResult(
               new ClientResponseBody(apiUser.Clients)
            );
        }

        /// <summary>
        /// Delete all api user clients
        /// </summary>
        /// <url>http://apiurl/apiUser/Id/clients</url>
        /// <param name="id">Identifier of api user</param>
        /// <response code="200">Returns 204 when api user clients was successfully deleted.</response>
        /// <response code="404">If record to delete was not found</response>   
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpDelete]
        [Route("{id}/clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClientsById(string id)
        {
            var apiUser = await _apiUserRepository.FindById(id);

            if (apiUser.IsEmpty)
            {
                return NotFound();
            }
            
            apiUser.RemoveAllClients();

            await _apiUserRepository.Update(apiUser);
            
            return NoContent();
        }
        
        /// <summary>
        /// Returns a subscription plan for specific api user
        /// </summary>
        /// <url>http://apiurl/apiUser/Id/subscriptionPlan</url>
        /// <param name="id">Identifier of api user</param>
        /// <response code="200">Returns 200 when api user subscription plan was successfully fetched.</response>
        /// <response code="404">If parent record (ApiUser) was not found</response>   
        /// <response code="500">Returns if there's an unhandled exception.</response>
        [HttpGet]
        [Route("{id}/subscriptionPlan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubscriptionPlan(string id)
        {
            var apiUser = await _apiUserRepository.FindById(id);

            return apiUser.IsEmpty
                ? NotFound()
                : new RestApiResult(
                    new SubscriptionPlanResponseBody(apiUser.SubscriptionPlan)
                );
        }
    }
}