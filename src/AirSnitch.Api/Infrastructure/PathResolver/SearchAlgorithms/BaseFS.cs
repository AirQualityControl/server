using AirSnitch.Api.Infrastructure.PathResolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver.SearchAlgorithms
{
    public abstract class BaseFS : ISearchAlgorithm
    {
        public abstract List<List<int>> ExecuteSearch(int startVertex, Graph graph);

        public virtual Dictionary<int, List<int>> GetRoute(Vertex sourceVertex, Graph graph)
        {
            var route = new Dictionary<int, List<int>>();

            int startVertex = graph.Vertices.IndexOf(sourceVertex);
            var path = ExecuteSearch(startVertex, graph);

            int index;

            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].Count > 0 && i != startVertex)
                {
                    index = i;
                    route.Add(i, new List<int>());
                    while (index != startVertex)
                    {
                        route[i].Add(index);
                        index = path[index][0];
                    }
                    route[i].Reverse();
                }
            }
            return route;
        }
    }
}
