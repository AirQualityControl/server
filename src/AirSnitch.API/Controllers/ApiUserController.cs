using System.Threading.Tasks;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.ApiUser;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
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
        
        public ApiUserController(
            DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph,
            IApiResourceRegistry apiResourceRegistry,
            IApiUserRepository apiUserRepository) : base(apiResourcesGraph, apiResourceRegistry)
        {
            _apiUserRepository = apiUserRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]RequestParameters requestParameters)
        {
            var queryScheme = GenerateQueryScheme(requestParameters.Includes, requestParameters.PageOptions);

            QueryResult result = await _apiUserRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            
            if (result.IsSuccess)
            {
                return new SuccessRestApiResult
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
        [Route("{apiUserId}")]
        public async Task<IActionResult> GetById(string apiUserId, string includedResources)
        {
            var queryScheme = GenerateQueryScheme(includedResources);
            
            queryScheme.AddColumnFilter(
                new EqualColumnFilter(
                     column:new PrimaryColumn(),
                     value:apiUserId
                    )
                );
            
            QueryResult result = await _apiUserRepository.ExecuteQueryFromSchemeAsync(queryScheme);
            
            if (result.IsSuccess)
            {
                return new SuccessRestApiResult
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
        [Route("{apiUserId}/clients")]
        public async Task<IActionResult> GetClients(string apiUserId)
        {
            return NotFound();
        }
        
        [HttpGet]
        [Route("{apiUserId}/subscriptionPlan")]
        public async Task<IActionResult> GetSubscriptionPlan(string apiUserId)
        {
            return NotFound();
        }
    }
}