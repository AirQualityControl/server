using System.Collections.Generic;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Relationship;

namespace AirSnitch.Api.Graph
{
    public class RelatedVertex<TValue> : Vertex<TValue> where TValue : IApiResourceMetaInfo
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