using System.Collections.Generic;
using AirSnitch.Api.Rest.Resources;

namespace AirSnitch.Api.Rest.Graph.TraversionStrategy
{
    internal class BfsGraphTraversion<TValue> : IGraphTraversionStrategy<TValue> where TValue : IApiResourceMetaInfo
    {
        private Queue<RelatedVertex<TValue>> _queue = new Queue<RelatedVertex<TValue>>();
        private List<RelatedVertex<TValue>> _visitedNode = new List<RelatedVertex<TValue>>();
        
        public IGraphTraversionStrategy<TValue> TraverseFrom(RelatedVertex<TValue> vertex)
        {
            _queue.Enqueue(vertex);
            TraverseInternal(vertex);
            return this;
        }

        public IReadOnlyCollection<RelatedVertex<TValue>> Result => _visitedNode;

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