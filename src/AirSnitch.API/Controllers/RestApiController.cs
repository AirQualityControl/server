using System.Collections.Generic;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    /// <summary>
    /// Base controller that helps to achieve restfulness
    /// </summary>
    public abstract class RestApiController : ControllerBase
    {
        private readonly DirectAcyclicGraph<IApiResourceMetaInfo> _apiResourcesGraph;
        
        /// <summary>
        /// Property that defines a target resource for every restfull controller.
        /// </summary>
        protected abstract IApiResourceMetaInfo CurrentResource { get; }
        protected RestApiController(DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph)
        {
            _apiResourcesGraph = apiResourcesGraph;
        }
        
        /// <summary>
        /// Returns a read-only collection of related resources(that might be fetched in single request) to target resource
        /// </summary>
        protected IReadOnlyCollection<IApiResourceMetaInfo> RelatedResources =>
            _apiResourcesGraph.GetAllReachableVertexFrom(
                new RelatedVertex<IApiResourceMetaInfo>(CurrentResource)
            );

        private QueryScheme BaseQueryScheme =>
            new GraphVisitor()
                .Visit(_apiResourcesGraph)
                .From(startingVertex: new RelatedVertex<IApiResourceMetaInfo>(CurrentResource))
                .BuildQueryScheme();

        /// <summary>
        /// Generate a query scheme that will be translated to query fot fetching the requested data.
        /// </summary>
        /// <param name="includedResources"></param>
        /// <returns></returns>
        protected QueryScheme GenerateQueryScheme(List<IApiResourceMetaInfo> includedResources)
        {
            var queryScheme = new GraphVisitor()
                .Visit(_apiResourcesGraph)
                .From(startingVertex: new RelatedVertex<IApiResourceMetaInfo>(CurrentResource))
                .Includes(includedResources)
                .BuildQueryScheme();

            return queryScheme;
        }
        
        /// <summary>
        /// Generate a query scheme that will be translated to query fot fetching the requested data.
        /// </summary>
        /// <param name="includedResources">Read-only collection of related resources that requested alongside with target resource </param>
        /// <param name="pageOptions">Client pageable options</param>
        /// <returns></returns>
        protected QueryScheme GenerateQueryScheme(List<IApiResourceMetaInfo> includedResources, PageOptions pageOptions)
        {
            var queryScheme = new GraphVisitor()
                .Visit(_apiResourcesGraph)
                .From(startingVertex: new RelatedVertex<IApiResourceMetaInfo>(CurrentResource))
                .Includes(includedResources)
                .BuildQueryScheme();
            
            queryScheme.AddPageOptions(pageOptions);
            
            return queryScheme;
        }
    }
}