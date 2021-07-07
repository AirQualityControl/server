using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Relationship;

namespace AirSnitch.Api.Graph
{
    public interface IDirectAcyclicGraph<TValue> where TValue : IApiResourceMetaInfo
    {
        bool ContainsVertex(RelatedVertex<IApiResourceMetaInfo> startingVertex);

        GraphPath<TValue> GetAllReachableVertexesFrom(RelatedVertex<TValue> rootVertex);

        void AddDirectedEdge(RelatedVertex<TValue> vertex1, RelatedVertex<TValue> vertex2,
            IApiResourceRelationship type);
    }
}