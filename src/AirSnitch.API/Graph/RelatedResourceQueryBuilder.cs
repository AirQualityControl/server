using System;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Relationship;
using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Api.Graph
{
    public class RelatedResourceQueryBuilder
    {
        private readonly FetchQuery _fetchQuery;
        public RelatedResourceQueryBuilder()
        {
            _fetchQuery = new FetchQuery();
        }
        public void AddRelation(IApiResourceMetaInfo rootResource, IApiResourceMetaInfo relatedResource,
            IApiResourceRelationship relationType)
        {
            if(relationType.GetType() == typeof(IncludeRelationship))
            {
                throw new Exception("unable to implement this type!");
            }

            if(_fetchQuery.EntityName == String.Empty)
            {
                _fetchQuery.EntityName = rootResource.Name.Value;

                AddColumns(rootResource);
                AddRelatedColumns(relatedResource);
            }
            else
            {
                AddRelatedColumns(relatedResource);
            } 
        }

        private void AddColumns(IApiResourceMetaInfo rootResource)
        {
            foreach (var column in rootResource.Columns)
            {
                _fetchQuery.AddColumn(new QueryColumn(
                    name: column.Name, 
                    path: column.Path)
                );
            }
        }
        private void AddRelatedColumns(IApiResourceMetaInfo relatedResource)
        {
            foreach (var column in relatedResource.Columns)
            {
                _fetchQuery.AddColumn(
                    new QueryColumn(
                        name:$"{relatedResource.Name}.{column}", 
                        path:$"{relatedResource.Name}.{column}")
                );
            }
        }
        public FetchQuery GenerateQuery(IApiResourceMetaInfo scalarResource)
        {
            _fetchQuery.EntityName = scalarResource.Name.Value;
            AddColumns(scalarResource);
            return _fetchQuery;
        }
        public FetchQuery FetchQuery => _fetchQuery;
    }
}