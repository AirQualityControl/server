using System.Collections.Generic;
using AirSnitch.Api.Rest.Resources;

namespace AirSnitch.Api.Rest.Graph.TraversionStrategy
{
    internal interface IGraphTraversionStrategy<TValue> where TValue : IApiResourceMetaInfo
    {
        IGraphTraversionStrategy<TValue> TraverseFrom(RelatedVertex<TValue> vertex);

        IReadOnlyCollection<RelatedVertex<TValue>> Result { get; }
    }
}