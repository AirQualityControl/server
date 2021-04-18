using AirSnitch.Api.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver
{
    public class ResourcePathResolver
    {
        private readonly BFS _bfs;
        private readonly Graph _graph;

        private readonly Dictionary<string, Dictionary<string, Resourse>> _resourseResolutionCache;

        
        public ResourcePathResolver()
        {
            _resourseResolutionCache = new Dictionary<string, Dictionary<string, Resourse>>();
            _bfs = new BFS();

            //TODO: inject graph to resolver Build graph on the statup of app 
            _graph = new Graph(6);
            var vertecies = new List<Vertex>
            {
                new Vertex { ResourseName = "City", ResoursePath = ControllersRoutes.City },
                new Vertex { ResourseName = "DataProvider", ResoursePath = ControllersRoutes.Dataprovider },
                new Vertex { ResourseName = "AirMonitoringStation", ResoursePath = ControllersRoutes.AirmonitoringStation },
                new Vertex { ResourseName = "AirPollutionHistory", ResoursePath = ControllersRoutes.AirPollutionHistory },
                new Vertex { ResourseName = "AirPollution", ResoursePath = ControllersRoutes.AirPolution },
                new Vertex { ResourseName = "User", ResoursePath = ControllersRoutes.User }
            };
            _graph.AddVertexRange(vertecies);
            _graph.AddEdge(vertecies[5], vertecies[2]);
            _graph.AddEdge(vertecies[4], vertecies[2]);
            _graph.AddEdge(vertecies[2], vertecies[0]);
            _graph.AddEdge(vertecies[2], vertecies[1]);
            _graph.AddEdge(vertecies[2], vertecies[3]);
        }

        public Dictionary<string, Resourse> GetResourses(string controllerPath)
        {
            var result = new Dictionary<string, Resourse>();
            if (!_resourseResolutionCache.ContainsKey(controllerPath))
            {
                var currentVertex = _graph.Vertices.Single(item => item.ResoursePath == controllerPath);
                var route = _bfs.GetRoute(currentVertex, _graph);
                foreach (var item in route)
                {
                    result.Add(_graph.Vertices[route[item.Key][^1]].ResoursePath, new Resourse
                    {
                        Path = route[item.Key].Select(item => "/" + _graph.Vertices[item].ResoursePath).Aggregate((x, y) => $"{x}{y}"),
                    });
                }
                result.Add("self", new Resourse { Path = "" });
                _resourseResolutionCache.Add(controllerPath, result);
                return result;
            }

            return _resourseResolutionCache[controllerPath];
        }

    }
}
