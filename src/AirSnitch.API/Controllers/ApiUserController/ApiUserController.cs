using System.Threading.Tasks;
using AirSnitch.Api.Controllers.ApiUser;
using AirSnitch.Api.Controllers.ApiUserController.Dto;
using AirSnitch.Api.Controllers.ApiUserController.ViewModels;
using AirSnitch.Api.Middleware.Authentication;
using AirSnitch.Api.Rest;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.ApiUser;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers.ApiUserController
{
    /// <summary>
    /// Controller that represent a ApiUser resource
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = Constants.Authentication.SchemeName, Policy = Constants.Authorization.InternalAppPolicyName)]
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
        ///     Returns all available api users.
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
        ///     Returns a requested api user by Id
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
        ///     Creates a new ApiUser in system
        /// </summary>
        /// <url>http://apiurl/apiUser/</url>
        /// <param name="apiUserDto">Valid api user data transfer model</param>
        /// <response code="200">Returns 200 when record was successfully added.Request body contains a unique identifier of created record</response>
        /// <response code="400">Returns 400(bad request) if clients submit invalid data.</response> 
        /// <response code="500">Returns in case of unhandled exception during delete operation.</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ApiUserDto apiUserDto)
        {
            Domain.Models.ApiUser apiUser = apiUserDto.CreateApiUser();

            if (await _apiUserRepository.IsUserAlreadyExists(apiUser))
            {
                return BadRequest($"User with email {apiUser.Profile.GetEmailValue()} already exists in the system!");
            }
            
            await _apiUserRepository.Add(apiUser);
            
            return Ok(
                new ApiUserCreatedResult()
                {
                    UserId = apiUser.Id
                });  
        }

        /// <summary>
        ///     Updates a whole api user record by specified id
        /// </summary>
        /// <param name="id">Valid api user identifier that exists in DB</param>
        /// <param name="apiUserDto">Valid api user data transfer model</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, ApiUserDto apiUserDto)
        {
            var updatedUserState = apiUserDto.CreateApiUser(id);

            var existingApiUser = await _apiUserRepository.FindById(id);

            if (existingApiUser.IsEmpty)
            {
                return NotFound($"User with id {id} does not exist");
            }
            
            existingApiUser.SetState(updatedUserState);
            
            await _apiUserRepository.Update(existingApiUser);
            return Ok();
        }

        /// <summary>
        ///     Delete an api user and all associated information with it.
        /// </summary>
        /// <url>http://apiurl/apiUser/Id</url>
        /// <param name="id">Identifier of api user to be deleted</param>
        /// <response code="204">Returns 204 when record was successfully deleted.</response>
        /// <response code="404">If record to delete was not found</response>   
        /// <response code="500">Returns in case of unhandled exception during delete operation.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteApiUserById(string id)
        {
            var deletionResult = await _apiUserRepository.Delete(id);

            return deletionResult == DeletionResult.Success ? NoContent() : NotFound();
        }
        
        
        /// <summary>
        ///     Add a new client for api user
        /// </summary>
        /// <url>http://apiurl/apiUser/Id/clients</url>
        /// <param name="id">Identifier of api user</param>
        /// <param name="apiClientDto">Valid model that represent an api client</param>
        /// <response code="200">Returns 204 when api user clients was successfully deleted.</response>
        /// <response code="404">If record to delete was not found</response>
        /// <response code="400">Bad request.Body contains the explanation what has happened </response>
        [HttpPost]
        [Route("{id}/clients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddClient(string id, ApiClientDto apiClientDto)
        {
            var client = apiClientDto.CreateApiClient();

            client.GenerateId();

            var apiUser = await _apiUserRepository.FindById(id);

            if (apiUser.IsEmpty)
            {
                return NotFound();
            }
            
            apiUser.AddClient(client);

            await _apiUserRepository.Update(apiUser);
            
            return Ok();
        }

        /// <summary>
        ///     Returns only clients for specific ApiUser
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
        ///     Delete all api user clients
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
        public async Task<IActionResult> DeleteAllClients(string id)
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
        ///     Returns a subscription plan for specific api user
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