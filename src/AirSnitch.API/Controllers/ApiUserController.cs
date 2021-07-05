using System.Threading.Tasks;
using AirSnitch.Api.Graph;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Graph;
using AirSnitch.Api.Resources.Relationship;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    [ApiController]
    [Route("apiUser")]
    public class ApiUserController : ControllerBase
    {
        private readonly IDirectAcyclicGraph<IApiResourceMetaInfo> _apiResourcesGraph;
        public ApiUserController(IDirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph)
        {
            _apiResourcesGraph = apiResourcesGraph;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var query = new GraphVisitor()
                .Visit(_apiResourcesGraph)
                .From(apiUserResourceVertex)
                .Includes(includedResources)
                .BuildQuery();
            
            
            //Page<Data> apiUsers = _apiUserRepository.ExecuteQuery(query);
            return new SuccessRestApiResult();
        }
        
        [HttpGet]
        [Route("{clientUserId}")]
        public SuccessRestApiResult GetById(string clientUserId)
        {
            //ApiUser user = _apiUserRepository.GetById(clientUserId);

            return new SuccessRestApiResult();
        }
    }
}