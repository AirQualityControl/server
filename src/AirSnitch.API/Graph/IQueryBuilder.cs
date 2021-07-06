using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Relationship;
using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Api.Graph
{
    public interface IQueryBuilder
    {
        void AddRelation(IApiResourceMetaInfo rootResource, IApiResourceMetaInfo relatedResource,
            IApiResourceRelationship relationType);

        Query GenerateQuery(IApiResourceMetaInfo scalarResource);

        Query Query { get; }
    }
}