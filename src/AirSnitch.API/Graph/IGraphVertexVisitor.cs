using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Graph;

namespace AirSnitch.Api.Graph
{
    internal interface IGraphVertexVisitor<TValue> where TValue : IApiResourceMetaInfo
    {
        void VisitVertex(RelatedVertex<TValue> value);
    }
}