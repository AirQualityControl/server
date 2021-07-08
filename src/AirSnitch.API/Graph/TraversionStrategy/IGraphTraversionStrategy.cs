using AirSnitch.Api.Resources;

namespace AirSnitch.Api.Graph.TraversionStrategy
{
    internal interface IGraphTraversionStrategy<TValue> where TValue : IApiResourceMetaInfo
    {
        void TraverseFrom(RelatedVertex<TValue> vertex);
    }
}