using AirSnitch.Api.Graph;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Graph;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    [ApiController]
    [Route("apiUser")]
    public class ApiUserController : ControllerBase
    {
        private readonly IDirectAcyclicGraph<IApiResourceMetaInfo> _apiResourcesGraph;
        private readonly IGraphVisitor _graphVisitor;

        //TODO: in base class
        private static RelatedVertex<IApiResourceMetaInfo> _currentResource = new RelatedVertex<IApiResourceMetaInfo>(new ApiUserResource());
        
        public ApiUserController(IDirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph,
            IGraphVisitor graphVisitor)
        {
            _apiResourcesGraph = apiResourcesGraph;
            _graphVisitor = graphVisitor;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var query = _graphVisitor
                .Visit(_apiResourcesGraph)
                .From(_currentResource)
                .Includes(null)
            .BuildQuery();

            //Page<Data> queryResult = await _apiUserRepository.ExecuteQueryAsync(query);
            
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