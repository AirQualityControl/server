using System.Collections.Generic;
using System.Threading.Tasks;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.ApiUser;
using AirSnitch.Api.Rest.Resources.Client;
using AirSnitch.Api.Rest.Resources.SubscriptionPlan;
using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence;
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
        
        public ApiUserController(DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, 
            IApiUserRepository apiUserRepository) : base(apiResourcesGraph)
        {
            _apiUserRepository = apiUserRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int pageSize, string includes)
        {
            var includedResources = new List<IApiResourceMetaInfo>()
            {
                new ClientApiResource(), 
                new SubscriptionPlanApiResource()
            };

            var queryScheme = GenerateQueryScheme(includedResources, new PageOptions(pageNumber:page, itemsPerPage:pageSize));

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
        public async Task<IActionResult> GetById(string apiUserId)
        {
            var includedResources = new List<IApiResourceMetaInfo>()
            {
                new ClientApiResource(), 
                new SubscriptionPlanApiResource()
            };

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
    }
}