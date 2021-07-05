using System.Collections.Generic;
using AirSnitch.Api.Resources.Relationship;

namespace AirSnitch.Api.Resources.Graph
{
    internal abstract class Vertex<TValue>  where TValue : IApiResourceMetaInfo
    {
        private readonly TValue _value;
        private List<RelatedVertex<TValue>> _neighbours;
        
        protected Vertex(TValue value)
        {
            _value = value;
            _neighbours = new List<RelatedVertex<TValue>>();
        }

        public void AddNeighbour(RelatedVertex<TValue> neighbourVertex)
        {
            _neighbours.Add(neighbourVertex);
        }

        public List<RelatedVertex<TValue>> Neighbours => _neighbours;

        public TValue Value => _value;

        protected bool Equals(Vertex<TValue> other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vertex<TValue>) obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    internal class RelatedVertex<TValue> : Vertex<TValue> where TValue : IApiResourceMetaInfo
    {
        private List<IApiResourceRelationship> _relations;
        public RelatedVertex(TValue value) : base(value)
        {
            _relations = new List<IApiResourceRelationship>();
        }
        
        public void AddRelation(IApiResourceRelationship relation)
        {
            _relations.Add(relation);
        }
    }
}