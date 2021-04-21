using AirSnitch.Api.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver.SearchAlgorithms
{
    public class BFS : BaseFS
    {
        public override List<List<int>> ExecuteSearch(int startVertex,  Graph graph)
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

    }
}
