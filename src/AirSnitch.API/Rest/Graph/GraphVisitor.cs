using System;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Relationship;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Rest.Graph
{
    public class GraphVisitor
    {
        private DirectAcyclicGraph<IApiResourceMetaInfo> _visitedGraph;
        private RelatedVertex<IApiResourceMetaInfo> _rootVertex;
        private readonly Queue<RelatedVertex<IApiResourceMetaInfo>> _queue;
        private readonly List<IApiResourceMetaInfo> _includedResources;
        private readonly RelatedResourceQueryBuilder _queryBuilder;
        
        public GraphVisitor()
        {
            _queryBuilder = new RelatedResourceQueryBuilder();
            _queue = new Queue<RelatedVertex<IApiResourceMetaInfo>>();
            _includedResources = new List<IApiResourceMetaInfo>();
        }
        
        public GraphVisitor Visit(DirectAcyclicGraph<IApiResourceMetaInfo> graph)
        {
            _visitedGraph = graph;
            return this;
        }
        public GraphVisitor From(RelatedVertex<IApiResourceMetaInfo> startingVertex)
        {
            if(!_visitedGraph.ContainsVertex(startingVertex))
            {
                throw new Exception("ex");
            }
            _rootVertex = _visitedGraph.GetVertex(startingVertex);
            return this;
        }

        public GraphVisitor Includes(IReadOnlyCollection<IApiResourceMetaInfo> includedResources)
        {
            foreach (var resource in includedResources)
            {
                _includedResources.Add(resource);
            }
            return this;
        }

        public QueryScheme BuildQueryScheme()
        {
            if (!_includedResources.Any())
            {
                return _queryBuilder.GenerateQuery(_rootVertex.Value);
            }
            
            VisitInternal(_rootVertex);
            return _queryBuilder.QueryScheme;
        }
        
        private void VisitInternal(RelatedVertex<IApiResourceMetaInfo> startingVertex)
        {
            foreach (var neighbour in startingVertex.Neighbours)
            {
                if (_includedResources.Any() && _includedResources.Contains(neighbour.Value))
                {
                    _queryBuilder.AddRelation(startingVertex.Value, neighbour.Value, new IncludeRelationship());
                    _includedResources.Remove(neighbour.Value);
                }
                
                if (!_queue.Contains(neighbour))
                {
                    _queue.Enqueue(neighbour);
                }
            }

            RelatedVertex<IApiResourceMetaInfo> _vertex;
            if (_queue.TryDequeue(out _vertex))
            {
                VisitInternal(_vertex);
            }
        }
    }
}