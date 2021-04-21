using AirSnitch.Api.Infrastructure.Attributes;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver
{
    public class GraphBuilder
    {
        private class VertexMetaData
        {
            public string Resourse { get; set; }

            public List<string> OutEdges { get; set; }
        }

        public static Graph BuildResourseGraph()
        {
            var controllersMetaInformation = Assembly.GetExecutingAssembly().GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ControllerBase)));

            var intermediateResult = new List<VertexMetaData>();
            foreach (var controllerType in controllersMetaInformation)
            {
                intermediateResult.Add(new VertexMetaData
                {
                    Resourse = ((RouteAttribute)controllerType.GetCustomAttribute(typeof(RouteAttribute))).Template,
                    OutEdges = controllerType.GetCustomAttributes(typeof(IncludeResourseAttribute))
                        .Select(item => ((IncludeResourseAttribute)item).Name).ToList()
                });
            }

            Dictionary<string, Vertex> vertecies = new();
            var graph = new Graph(intermediateResult.Count);
            foreach (var item in intermediateResult)
            {
                var vertex = new Vertex { ResourseName = item.Resourse };
                vertecies.Add(item.Resourse, vertex);
                graph.AddVertex(vertex);
            }

            foreach (var item in intermediateResult)
            {
                foreach (var vertexKey in item.OutEdges)
                {
                    if (vertecies.TryGetValue(vertexKey, out _))
                    {
                        graph.AddEdge(vertecies[item.Resourse], vertecies[vertexKey]);
                    }
                    else
                    {
                        throw new ArgumentException($"You cannot use IncludeResourseAttribute with Name: {vertexKey} which" +
                            " is not represented by controller with the same RouteAttribute");
                    }
                }
            }
            return graph;
        }
    }
}
