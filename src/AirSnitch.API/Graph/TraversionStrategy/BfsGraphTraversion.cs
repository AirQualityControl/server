using System.Collections.Generic;
using ApiResourcesGrapth;
using ApiResourcesGrapth.Graph;

namespace AirSnitch.Api.Resources.Graph.TraversionStrategy
{
    internal class BfsGraphTraversion<TValue> : IGraphTraversionStrategy<TValue> where TValue : IApiResourceMetaInfo
    {
        private Queue<RelatedVertex<TValue>> _queue = new Queue<RelatedVertex<TValue>>();
        private List<RelatedVertex<TValue>> _visitedNode = new List<RelatedVertex<TValue>>();
        
        public GraphPath<TValue> TraverseFrom(RelatedVertex<TValue> vertex)
        {
            _queue.Enqueue(vertex);
            TraverseInternal(vertex);

            return GraphPath<TValue>.CreateFrom(_visitedNode);
        }

        private void TraverseInternal(RelatedVertex<TValue> vertex)
        {
            foreach (var neighbour in vertex.Neighbours)
            {
                if (!_queue.Contains(neighbour))
                {
                    _visitedNode.Add(neighbour);
                    _queue.Enqueue(neighbour);
                }
            }

            RelatedVertex<TValue> _vertex;
            if (_queue.TryDequeue(out _vertex))
            {
                TraverseInternal(_vertex);
            }
        }
    }
}