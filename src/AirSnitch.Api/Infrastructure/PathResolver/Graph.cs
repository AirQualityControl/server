using AirSnitch.Api.Infrastructure.PathResolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver
{
    public class Graph
    {
        public List<Vertex> Vertices { get; private set; }
        public int[,] AdjacencyMatrix { get; private set; }

        public Graph(int numberOfVerticies)
        {
            AdjacencyMatrix = new int[numberOfVerticies, numberOfVerticies];
            Vertices = new List<Vertex>();
        }

        public void AddVertex(Vertex vertex)
        {
            Vertices.Add(vertex);
        }

        public void AddVertexRange(IEnumerable<Vertex> vertecies)
        {
            Vertices.AddRange(vertecies);
        }

        public void AddEdge(Vertex sourseVertex, Vertex targetVertex)
        {
            int sourseVertexIndex = Vertices.IndexOf(sourseVertex);
            int targetVertexIndex = Vertices.IndexOf(targetVertex);
            AdjacencyMatrix[sourseVertexIndex, targetVertexIndex] = 1;
        }
    }
}
