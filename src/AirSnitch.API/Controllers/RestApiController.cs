using System.Collections.Generic;
using AirSnitch.Api.Graph;
using AirSnitch.Api.Resources;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers
{
    public abstract class RestApiController : ControllerBase
    {
        private readonly DirectAcyclicGraph<IApiResourceMetaInfo> _apiResourcesGraph;
        protected abstract IApiResourceMetaInfo CurrentResource { get; }
        protected RestApiController(DirectAcyclicGraph<IApiResourceMetaInfo> apiResourcesGraph)
        {
            _apiResourcesGraph = apiResourcesGraph;
        }
        
        protected IReadOnlyCollection<IApiResourceMetaInfo> RelatedResources =>
            _apiResourcesGraph.GetAllReachableVertexFrom(
                new RelatedVertex<IApiResourceMetaInfo>(CurrentResource)
            );

        private QueryScheme BaseQueryScheme =>
            new GraphVisitor()
                .Visit(_apiResourcesGraph)
                .From(startingVertex: new RelatedVertex<IApiResourceMetaInfo>(CurrentResource))
                .BuildQueryScheme();

        protected QueryScheme GenerateQueryScheme(List<IApiResourceMetaInfo> includedResources)
        {
            var queryScheme = new GraphVisitor()
                .Visit(_apiResourcesGraph)
                .From(startingVertex: new RelatedVertex<IApiResourceMetaInfo>(CurrentResource))
                .Includes(includedResources)
                .BuildQueryScheme();

            return queryScheme;
        }
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