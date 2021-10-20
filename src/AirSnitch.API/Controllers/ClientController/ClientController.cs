using System.Threading.Tasks;
using AirSnitch.Api.Controllers.ApiUserController.Dto;
using AirSnitch.Api.Controllers.ClientController.ViewModel;
using AirSnitch.Api.Rest;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Client;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers.ClientController
{
    [ApiController]
    [Route("client")]
    public class ClientController : RestApiController
    {
        private readonly IClientRepository _clientRepository;
        private readonly IApiUserRepository _apiUserRepository;
        
        public ClientController(
            DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, 
            IApiResourceRegistry apiResourceRegistry,
            IClientRepository clientRepository, IApiUserRepository apiUserRepository) : base(apiResourcesGraph, apiResourceRegistry)
        {
            _clientRepository = clientRepository;
            _apiUserRepository = apiUserRepository;
        }

        protected override IApiResourceMetaInfo CurrentResource => new ClientApiResource();
        
        /// <summary>
        /// Returns a specific client by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includedResources"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id, string includedResources)
        {
            var queryScheme = GenerateQueryScheme(includedResources);
            
            queryScheme.AddColumnFilter(
                new EqualColumnFilter(
                    column: CurrentResource.QueryColumn,
                    value:id
                )
            );
            
            QueryResult result = await _clientRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            
            if (result.IsSuccess)
            {
                return new RestApiResult
                (
                    new RestResponseBody(
                        Request,
                        result,
                        RelatedResources,
                        base.GetIncludedResources(includedResources)
                    )
                );
            }

            return new NotFoundResult();
        }

        /// <summary>
        /// Delete a specific client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var apiUser = await _clientRepository.FindClientOwner(clientId: id);

            if (apiUser.IsEmpty)
            {
                return NotFound();
            }

            apiUser.RemoveClientById(id);

            await _apiUserRepository.Update(apiUser);

            return NoContent();
        }

        /// <summary>
        /// Updates an existing api client record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="apiClientDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, ApiClientDto apiClientDto)
        {
            var newClientState = apiClientDto.CreateApiClient();

            Domain.Models.ApiUser apiUser = await _clientRepository.FindClientOwner(id);

            if (apiUser.IsEmpty)
            {
                return NotFound();
            }

            var targetClient = apiUser.GetClient(clientId: id);
            
            targetClient.SetState(newClientState);

            await _apiUserRepository.Update(apiUser);
            
            return Ok();
        }

        
        /// <summary>
        /// Revoke api key for specified api client
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/apiKey")]
        public async Task<IActionResult> RevokeApiKey(string id)
        {
            ApiClient client = await _clientRepository.GetById(id);

            if (client.IsEmpty)
            {
                return NotFound();
            }

            client.RevokeApikey();

            await _clientRepository.Update(client);

            return Ok();
        }

        /// <summary>
        /// Generate api key for specified api client.
        /// Keep in mind in case if api key exists it will be override 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/apiKey")]
        public async Task<IActionResult> GenerateApiKey(string id)
        {
            ApiClient client = await _clientRepository.GetById(id);

            if (client.IsEmpty)
            {
                return NotFound();
            }

            var apiKey = client.RegenerateApiKey();

            await _clientRepository.Update(client);

            return Ok(new ApiKeyCreatedResult()
            {
                Value = apiKey.Value,
                ExpiryDate = apiKey.ExpirationDate,
                IssueDate = apiKey.IssueDate
            });
        }
    }
}