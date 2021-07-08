using System.Collections.Generic;
using System.Linq;
using AirSnitch.Api.Graph.TraversionStrategy;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Relationship;

namespace AirSnitch.Api.Graph
{
    public class DirectAcyclicGraph<TValue> where TValue : IApiResourceMetaInfo 
    {
        private readonly List<RelatedVertex<TValue>> _adjacencylist;
        private readonly IGraphTraversionStrategy<TValue> _graphTraversingStrategy;
        public DirectAcyclicGraph(int numberOfNode)
        {
            _adjacencylist = new List<RelatedVertex<TValue>>(numberOfNode);
            _graphTraversingStrategy = new BfsGraphTraversion<TValue>();
        }
        
        public void AddDirectedEdge(RelatedVertex<TValue> vertex1, RelatedVertex<TValue> vertex2, IApiResourceRelationship type)
        {
            vertex1.AddNeighbour(vertex2);
            vertex1.AddRelation(type);
            
            AddToAdjacencyList(vertex1);
        }
        private void AddToAdjacencyList(RelatedVertex<TValue> vertex1)
        {
            if (!_adjacencylist.Contains(vertex1))
            {
                _adjacencylist.Add(vertex1);
            }
        }

        public bool ContainsVertex(RelatedVertex<IApiResourceMetaInfo> startingVertex)
        {
            return true;
        }

        public RelatedVertex<TValue> GetVertex(RelatedVertex<TValue> vertex)
        {
            return _adjacencylist
                .Single(v => v.Value.Equals(vertex.Value));
        }
    }
}