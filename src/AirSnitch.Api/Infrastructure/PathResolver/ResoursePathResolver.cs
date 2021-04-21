using AirSnitch.Api.Infrastructure.Attributes;
using AirSnitch.Api.Infrastructure.Common;
using AirSnitch.Api.Infrastructure.Interfaces;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver
{
    public class ResoursePathResolver : IResoursePathResolver
    {
        private readonly ISearchAlgorithm _searchAlgorithm;
        private readonly Graph _graph;

        private readonly Dictionary<string, Dictionary<string, Resourse>> _resourseResolutionCache;

        public ResoursePathResolver(ISearchAlgorithm searchAlgorithm, Graph graph)
        {
            _resourseResolutionCache = new Dictionary<string, Dictionary<string, Resourse>>();
            _searchAlgorithm = searchAlgorithm;
            _graph = graph;
        }

        
        public Dictionary<string, Resourse> GetResourses(string controllerPath)
        {
            var result = new Dictionary<string, Resourse>();
            if (!_resourseResolutionCache.ContainsKey(controllerPath))
            {
                var currentVertex = _graph.Vertices.Single(item => item.ResourseName == controllerPath);
                var route = _searchAlgorithm.GetRoute(currentVertex, _graph);
                foreach (var item in route)
                {
                    result.Add(_graph.Vertices[route[item.Key][^1]].ResourseName, new Resourse
                    {
                        Path = route[item.Key].Select(item => "/" + _graph.Vertices[item].ResourseName).Aggregate((x, y) => $"{x}{y}"),
                    });
                }
                result.Add("self", new Resourse { Path = "" });
                _resourseResolutionCache.Add(controllerPath, result);
                return result;
            }

            return _resourseResolutionCache[controllerPath];
        }

        public bool IsQueryPathValid(string controllerPath, int id, string queryPath)
        {
            var currentResourse = new Resourse { Path = "/" + queryPath };
            if (!_resourseResolutionCache.ContainsKey(controllerPath))
            {
                var resourses = GetResourses(controllerPath);
                return resourses.Values.Contains(currentResourse);
            }
            return _resourseResolutionCache[controllerPath].Values.Contains(currentResourse);
        }

    }
}
