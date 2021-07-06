using System.Collections.Generic;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Graph;
using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Api.Graph
{
    public interface IGraphVisitor
    {
        IGraphVisitor Visit(IDirectAcyclicGraph<IApiResourceMetaInfo> graph);
        
        IGraphVisitor From(RelatedVertex<IApiResourceMetaInfo> rootVertex);

        IGraphVisitor Includes(IReadOnlyCollection<IApiResourceMetaInfo> includedResources);

        Query BuildQuery();
    }
}