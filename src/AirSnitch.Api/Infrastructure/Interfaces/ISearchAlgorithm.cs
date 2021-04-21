using AirSnitch.Api.Infrastructure.PathResolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver
{
    public interface ISearchAlgorithm
    {
        Dictionary<int, List<int>> GetRoute(Vertex sourceVertex, Graph graph);
    }

    [ContractClassFor(typeof(ISearchAlgorithm))]
    public abstract class SearchAlgorithmContract : ISearchAlgorithm
    {
        public Dictionary<int, List<int>> GetRoute(Vertex sourceVertex, Graph graph)
        {
            Contract.Requires(graph != null);
            Contract.Requires(graph.Vertices != null);
            Contract.Requires(graph.AdjacencyMatrix.Length > 0);
            Contract.Requires(graph.Vertices.Contains(sourceVertex));

            return default;
        }
    }
}
