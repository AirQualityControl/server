using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Graph;

namespace AirSnitch.Api.Graph
{
    internal interface IGraphTraversionStrategy<TValue> where TValue : IApiResourceMetaInfo
    {
        GraphPath<TValue> TraverseFrom(RelatedVertex<TValue> vertex);
    }
}