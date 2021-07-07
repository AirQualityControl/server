using System.Collections.Generic;
using AirSnitch.Api.Graph;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.ApiUser;
using AirSnitch.Api.Resources.Client;
using AirSnitch.Api.Resources.SubscriptionPlan;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    [ApiController]
    [Route("apiUser")]
    public class ApiUserController : ControllerBase
    {
        private readonly DirectAcyclicGraph<IApiResourceMetaInfo> _apiResourcesGraph;
        //TODO: in base class
        private static RelatedVertex<IApiResourceMetaInfo> _currentResource = 
            new RelatedVertex<IApiResourceMetaInfo>(new ApiUserResource());
        
        public ApiUserController(DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph)
        {
            _apiResourcesGraph = apiResourcesGraph;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var requestedResources = new List<IApiResourceMetaInfo>()
                {
                    new ClientApiResource(), 
                    new SubscriptionPlanApiResource()
                };
            
            var queryScheme = new GraphVisitor()
                .Visit(_apiResourcesGraph)
                .From(startingVertex: _currentResource)
                .Includes(requestedResources)
            .BuildQueryScheme();
            
            //Query query = _querySchemeInterpreter.BuildQuery(queryScheme);
            
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