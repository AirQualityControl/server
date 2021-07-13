using AirSnitch.Api.Rest.Resources;

namespace AirSnitch.Api.Rest.Graph.TraversionStrategy
{
    internal interface IGraphTraversionStrategy<TValue> where TValue : IApiResourceMetaInfo
    {
        void TraverseFrom(RelatedVertex<TValue> vertex);
    }
}