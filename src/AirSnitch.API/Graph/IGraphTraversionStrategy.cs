namespace AirSnitch.Api.Resources.Graph
{
    internal interface IGraphTraversionStrategy<TValue> where TValue : IApiResourceMetaInfo
    {
        GraphPath<TValue> TraverseFrom(RelatedVertex<TValue> vertex);
    }
}