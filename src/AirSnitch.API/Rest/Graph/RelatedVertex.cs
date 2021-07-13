using System.Collections.Generic;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.Resources.Relationship;

namespace AirSnitch.Api.Rest.Graph
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