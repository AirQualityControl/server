using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Api.Resources.Graph
{
    internal class GraphPath<TValue> : IEnumerable<TValue> where TValue : IApiResourceMetaInfo
    {
        private readonly IReadOnlyCollection<RelatedVertex<TValue>> _path;
        
        private GraphPath(IReadOnlyCollection<RelatedVertex<TValue>> path)
        {
            _path = path;
        }

        public Query EmmitQuery()
        {
            return null;
        }
        
        internal static GraphPath<TValue> CreateFrom(IReadOnlyCollection<RelatedVertex<TValue>> collection)
        {
            return new GraphPath<TValue>(collection);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _path.Select(v => v.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}