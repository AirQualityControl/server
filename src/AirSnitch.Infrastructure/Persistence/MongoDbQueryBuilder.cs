using System;
using AirSnitch.Api.Graph;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Relationship;
using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Infrastructure.Persistence
{
    public class MongoDbQueryBuilder : IQueryBuilder
    {
        public void AddRelation(IApiResourceMetaInfo rootResource, IApiResourceMetaInfo relatedResource,
            IApiResourceRelationship relationType)
        {
            throw new System.NotImplementedException();
        }

        public Query GenerateQuery(IApiResourceMetaInfo scalarResource)
        {
            throw new NotImplementedException();
        }

        public Query Query => throw new NotImplementedException("");
    }
}