using AirSnitch.Api.Resources;

namespace AirSnitch.Api.Graph
{
    internal interface IGraphTraversionStrategy<TValue> where TValue : IApiResourceMetaInfo
    {
        GraphPath<TValue> TraverseFrom(RelatedVertex<TValue> vertex);
    }
}