using System;
using System.Collections.Generic;
using AirSnitch.Api.Rest.Graph;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Registry;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    /// <summary>
    /// Base controller that helps to achieve restfulness
    /// </summary>
    public abstract class RestApiController : ControllerBase
    {
        private readonly DirectAcyclicGraph<IApiResourceMetaInfo> _apiResourcesGraph;
        private readonly IApiResourceRegistry _apiResourceRegistry;

        /// <summary>
        /// Property that defines a target resource for every restfull controller.
        /// </summary>
        protected abstract IApiResourceMetaInfo CurrentResource { get; }
        protected RestApiController(DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph, IApiResourceRegistry apiResourceRegistry)
        {
            _apiResourcesGraph = apiResourcesGraph;
            _apiResourceRegistry = apiResourceRegistry;
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
        /// <param name="includedResourcesString"></param>
        /// <returns></returns>
        protected QueryScheme GenerateQueryScheme(string includedResourcesString)
        {
            var predicate = BuildPredicate(includedResourcesString);
            
            var includedResources = _apiResourceRegistry.GetBy(predicate);
            
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
        /// <param name="includesString">Read-only collection of related resources that requested alongside with target resource </param>
        /// <param name="pageOptions">Client pageable options</param>
        /// <returns></returns>
        protected QueryScheme GenerateQueryScheme(string includesString, PageOptions pageOptions)
        {
            var predicate = BuildPredicate(includesString);
            
            var includedResources = _apiResourceRegistry.GetBy(predicate);
            
            var queryScheme = new GraphVisitor()
                .Visit(_apiResourcesGraph)
                .From(startingVertex: new RelatedVertex<IApiResourceMetaInfo>(CurrentResource))
                .Includes(includedResources)
                .BuildQueryScheme();
            
            queryScheme.AddPageOptions(pageOptions);
            
            return queryScheme;
        }

        private static Func<IApiResourceMetaInfo, bool> BuildPredicate(string includesString) 
        {
            if (string.IsNullOrEmpty(includesString))
            {
                return (_) => false;
            }
            return (resource) => includesString.Contains(resource.Name.Value);
        }
    }
}