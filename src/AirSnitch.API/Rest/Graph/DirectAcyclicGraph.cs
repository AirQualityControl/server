using System.Collections.Generic;
using System.Linq;
using AirSnitch.Api.Rest.Graph.TraversionStrategy;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Relationship;

namespace AirSnitch.Api.Rest.Graph
{
    /// <summary>
    /// ADT that represent a classic DAG of all API resources.
    /// DirectAcyclicGraph is responsible for any related resource lookups.
    /// This class has singleton lifecycle and spin-up on application start.
    /// </summary>
    /// <typeparam name="TValue">Value that DAG holds</typeparam>
    public sealed class DirectAcyclicGraph<TValue> where TValue : IApiResourceMetaInfo 
    {
        private readonly List<RelatedVertex<TValue>> _adjacencylist;
        public DirectAcyclicGraph(int numberOfNode)
        {
            _adjacencylist = new List<RelatedVertex<TValue>>(numberOfNode);
        }

        private IGraphTraversionStrategy<TValue> TraversionStrategy => new BfsGraphTraversion<TValue>();

        /// <summary>
        /// Add a direct edge in Graph.
        /// </summary>
        /// <param name="vertex1">Staring vertex.</param>
        /// <param name="vertex2">End vertex</param>
        /// <param name="type">Logical Relationship between those 2 vertex</param>
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

        /// <summary>
        /// Method that determines whether current instance of DAG contains certain vertex or not.
        /// </summary>
        /// <param name="startingVertex"></param>
        /// <returns></returns>
        public bool ContainsVertex(RelatedVertex<IApiResourceMetaInfo> startingVertex)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public RelatedVertex<TValue> GetVertex(RelatedVertex<TValue> vertex)
        {
            return _adjacencylist
                .Single(v => v.Value.Equals(vertex.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool TryGetVertex(out RelatedVertex<TValue> vertex)
        {
            vertex = null;
            return true;
        }

        /// <summary>
        /// Method that returns all reachable vertexes from current vertex.
        /// Result read-only collection will determine what resource could be requested together
        /// </summary>
        /// <param name="requestedVertex">Starting vertex to getting all relatives</param>
        /// <returns></returns>
        public IReadOnlyCollection<TValue> GetAllReachableVertexFrom(RelatedVertex<TValue> requestedVertex)
        {
            var rootVertex = GetVertex(requestedVertex);

            var reachableVertices = TraversionStrategy.TraverseFrom(rootVertex).Result;

            return reachableVertices.Select(vertex => vertex.Value).ToList();
        }
    }
}