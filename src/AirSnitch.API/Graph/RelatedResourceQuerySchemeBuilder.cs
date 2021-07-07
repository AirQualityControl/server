using System;
using AirSnitch.Api.Resources;
using AirSnitch.Api.Resources.Relationship;
using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Api.Graph
{
    public class RelatedResourceQueryBuilder
    {
        private readonly QueryScheme _queryScheme;
        public RelatedResourceQueryBuilder()
        {
            _queryScheme = new QueryScheme();
        }
        public void AddRelation(IApiResourceMetaInfo rootResource, IApiResourceMetaInfo relatedResource,
            IApiResourceRelationship relationType)
        {
            if(relationType.GetType() != typeof(IncludeRelationship))
            {
                throw new Exception("unable to implement this type!");
            }

            if(_queryScheme.EntityName == null)
            {
                _queryScheme.EntityName = rootResource.Name.Value;

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
                _queryScheme.AddColumn(new QueryColumn(
                    name: column.Name, 
                    path: column.Path)
                );
            }
        }
        private void AddRelatedColumns(IApiResourceMetaInfo relatedResource)
        {
            foreach (var column in relatedResource.Columns)
            {
                _queryScheme.AddColumn(
                    new QueryColumn(
                        name:$"{relatedResource.Name.Value}.{column.Name}", 
                        path:$"{relatedResource.Name.Value}.{column.Name}")
                );
            }
        }
        public QueryScheme GenerateQuery(IApiResourceMetaInfo scalarResource)
        {
            _queryScheme.EntityName = scalarResource.Name.Value;
            AddColumns(scalarResource);
            return _queryScheme;
        }
        public QueryScheme QueryScheme => _queryScheme;
    }
}