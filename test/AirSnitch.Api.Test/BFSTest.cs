
using AirSnitch.Api.Infrastructure.PathResolver;
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
                new Vertex { ResourseName = "City", ResoursePath = "city" },
                new Vertex { ResourseName = "DataProvider", ResoursePath = "dataprovider" },
                new Vertex { ResourseName = "AirMonitoringStation", ResoursePath = "airmonitoringstation" },
                new Vertex { ResourseName = "AirPollutionHistory", ResoursePath = "airpollutionhistory" },
                new Vertex { ResourseName = "AirPollution", ResoursePath = "airpollution" },
                new Vertex { ResourseName = "User", ResoursePath = "user" }
            };
            graph.AddVertexRange(vertecies);
            graph.AddEdge(vertecies[5], vertecies[2]);
            graph.AddEdge(vertecies[4], vertecies[2]);
            graph.AddEdge(vertecies[2], vertecies[0]);
            graph.AddEdge(vertecies[2], vertecies[1]);
            graph.AddEdge(vertecies[2], vertecies[3]);

            var result = _bfs.GetRoute(vertecies[4], graph);

            Assert.AreEqual(result, result);
        }
    }
}

