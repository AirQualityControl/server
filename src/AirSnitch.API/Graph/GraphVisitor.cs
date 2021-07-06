using System.Collections.Generic;
using System.Linq;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Graph;
using AirSnitch.Api.Resources.Relationship;
using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Api.Graph
{
    public class GraphVisitor<TValue> : IGraphVisitor
    {
        private IDirectAcyclicGraph<IApiResourceMetaInfo> _visitedGraph;
        private RelatedVertex<IApiResourceMetaInfo> _rootVertex;
        private readonly Queue<RelatedVertex<IApiResourceMetaInfo>> _queue;
        private readonly List<IApiResourceMetaInfo> _includedResources;
        private readonly IQueryBuilder _queryBuilder;
        
        public GraphVisitor(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
            _queue = new Queue<RelatedVertex<IApiResourceMetaInfo>>();
            _includedResources = new List<IApiResourceMetaInfo>();
        }
        
        public IGraphVisitor Visit(IDirectAcyclicGraph<IApiResourceMetaInfo> graph)
        {
            _visitedGraph = graph;
            return this;
        }
        public IGraphVisitor From(RelatedVertex<IApiResourceMetaInfo> rootVertex)
        {
            _rootVertex = rootVertex;
            return this;
        }

        public IGraphVisitor Includes(IReadOnlyCollection<IApiResourceMetaInfo> includedResources)
        {
            foreach (var resource in includedResources)
            {
                _includedResources.Add(resource);
            }
            return this;
        }

        public Query BuildQuery()
        {
            if (!_includedResources.Any())
            {
                return _queryBuilder.GenerateQuery(_rootVertex.Value);
            }
            VisitInternal(_rootVertex);
            return _queryBuilder.Query;
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