
using AirSnitch.Api.Infrastructure.PathResolver;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using AirSnitch.Api.Infrastructure.PathResolver.SearchAlgorithms;
using NUnit.Framework;
using System.Collections.Generic;

namespace AirSnitch.Api.Test
{
    [TestFixture]
    public class BFSTest
    {
        private BFS _bfs;
        [SetUp]
        public void Setup()
        {
            _bfs = new BFS();
        }

        [Test]
        public void GetRouteTest()
        {


            var graph = new Graph(6);
            var vertecies = new List<Vertex>
            {
                new Vertex { ResourseName = "city" },
                new Vertex { ResourseName = "dataprovider" },
                new Vertex { ResourseName = "airmonitoringstation" },
                new Vertex { ResourseName = "airpollutionhistory" },
                new Vertex { ResourseName = "airpollution" },
                new Vertex { ResourseName = "user" }
            };
            graph.AddVertexRange(vertecies);
            graph.AddEdge(vertecies[5], vertecies[2]);
            graph.AddEdge(vertecies[4], vertecies[2]);
            graph.AddEdge(vertecies[2], vertecies[0]);
            graph.AddEdge(vertecies[2], vertecies[1]);
            graph.AddEdge(vertecies[2], vertecies[3]);

            var expectedResult = new Dictionary<int, List<int>>
            {
                [0] = new List<int> { 2, 0 },
                [1] = new List<int> { 2, 1 },
                [2] = new List<int> { 2 },
                [3] = new List<int> { 2, 3 }
            };

            var result = _bfs.GetRoute(vertecies[4], graph);

            Assert.AreEqual(expectedResult, result);
        }
    }
}

