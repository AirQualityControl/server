using AirSnitch.Api.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver
{
    public class BFS 
    {
        public virtual List<List<int>> ExecuteSearch(int startVertex,  Graph graph)
        {
            int[] _reachedVertecies;
            int[] _numbersOfReachedVertecies;
            int N;
            List<List<int>> path = new List<List<int>>();

            N = graph.AdjacencyMatrix.GetLength(0);
            for (int k = 0; k < N; k++)
            {
                path.Add(new List<int>());
            }

            _reachedVertecies = new int[N];
            _numbersOfReachedVertecies = new int[N * N];
            int i = 0;
            int g = -1;

            _reachedVertecies[startVertex] = 1;
            _numbersOfReachedVertecies[i] = startVertex;
            path[startVertex].Add(startVertex);
            while (g < i)
            {
                g++;
                for (int j = 0; j < N; j++)
                {
                    if (_reachedVertecies[j] == 0 && graph.AdjacencyMatrix[_numbersOfReachedVertecies[g], j] == 1)
                    {
                        i++;
                        _reachedVertecies[j] = 1;
                        _numbersOfReachedVertecies[i] = j;
                        path[j].Add(_numbersOfReachedVertecies[g]);
                    }
                }
            }
            return path;
        }

        public virtual Dictionary<int, List<int>> GetRoute(Vertex sourceVertex, Graph graph)
        {
            var route = new Dictionary<int, List<int>>();
            var result = new List<Resourse>();

            int startVertex = graph.Vertices.IndexOf(sourceVertex);
            var path = ExecuteSearch(startVertex, graph);

            int index;

            for (int i = 0; i<path.Count; i++)
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
