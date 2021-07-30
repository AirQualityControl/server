using System.Threading.Tasks;
using AirSnitch.Api.Rest;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Client;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers.Client
{
    
    [ApiController]
    [Route("client")]
    public class ClientController : RestApiController
    {
        private readonly IClientRepository _clientRepository;
        
        public ClientController(
            DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, 
            IApiResourceRegistry apiResourceRegistry,
            IClientRepository clientRepository) : base(apiResourcesGraph, apiResourceRegistry)
        {
            _clientRepository = clientRepository;
        }

        protected override IApiResourceMetaInfo CurrentResource => new ClientApiResource();
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestParameters requestParameters)
        {
            var queryScheme = GenerateQueryScheme(requestParameters.Includes, requestParameters.PageOptions);

            QueryResult result = await _clientRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            
            //TODO: wrap into rest api result
            if (result.IsSuccess)
            {
                return new RestApiResult
                (
                    new RestResponseBody(
                        Request,
                        result,
                        RelatedResources
                    )
                );
            }
            return new NotFoundResult();
        }

        [HttpGet]
        [Route("Id")]
        public async Task<IActionResult> GetById(string clientId, string includedResources)
        {
            var queryScheme = GenerateQueryScheme(includedResources);
            
            queryScheme.AddColumnFilter(
                new EqualColumnFilter(
                    column: CurrentResource.QueryColumn,
                    value:clientId
                )
            );
            
            QueryResult result = await _clientRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            
            //TODO: wrap into rest api result
            if (result.IsSuccess)
            {
                return new RestApiResult
                (
                    new RestResponseBody(
                        Request,
                        result,
                        RelatedResources
                    )
                );
            }

            return new NotFoundResult();
        }
    }
}