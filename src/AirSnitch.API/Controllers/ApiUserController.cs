using System.Collections.Generic;
using System.Threading.Tasks;
using AirSnitch.Api.Graph;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.ApiUser;
using AirSnitch.Api.Resources.Client;
using AirSnitch.Api.Resources.SubscriptionPlan;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    [ApiController]
    [Route("apiUser")]
    public class ApiUserController : RestApiController
    {
        private readonly IApiUserRepository _apiUserRepository;
        protected override IApiResourceMetaInfo CurrentResource => new ApiUserResource();
        
        public ApiUserController(DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, 
            IApiUserRepository apiUserRepository) : base(apiResourcesGraph)
        {
            _apiUserRepository = apiUserRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var includedResources = new List<IApiResourceMetaInfo>()
            {
                new ClientApiResource(), 
                new SubscriptionPlanApiResource()
            };

            var queryScheme = GenerateQueryScheme(includedResources, new PageOptions(pageNumber:1));

            QueryResult result = await _apiUserRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            
            if (result.IsSuccess)
            {
                return new SuccessRestApiResult(new RestResponseBody(result.Value), RelatedResources);
            }
            return new NotFoundResult();
        }
        
        [HttpGet]
        [Route("{clientUserId}")]
        public SuccessRestApiResult GetById(string clientUserId)
        {
            //ApiUser user = _apiUserRepository.GetById(clientUserId);

            return new SuccessRestApiResult(null, null);
        }
    }
}