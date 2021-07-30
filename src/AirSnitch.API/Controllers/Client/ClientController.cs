using System.Threading.Tasks;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Client;
using AirSnitch.Api.Rest.Resources.Registry;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers.Client
{
    
    [ApiController]
    [Route("client")]
    public class ClientController : RestApiController
    {
        public ClientController(
            DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, 
            IApiResourceRegistry apiResourceRegistry) : base(apiResourcesGraph, apiResourceRegistry)
        {
            
        }

        protected override IApiResourceMetaInfo CurrentResource => new ClientApiResource();


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestParameters requestParameters)
        {
            var queryScheme = GenerateQueryScheme(requestParameters.Includes, requestParameters.PageOptions);

            return Ok();
        }

        [HttpGet]
        [Route("Id")]
        public async Task<IActionResult> GetById(string clientId, string includedResources)
        {
            return Ok();
        }
    }
}