using System;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Filters;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.Filters;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Query
{
    public class MongoDbQuery : IQuery
    {
        private readonly BsonDocument _projection;
        private BsonDocument _filters;

        //TODO: separate class
        public BsonDocument Projection => _projection;

        //TODO: separate class
        public BsonDocument Filter {
            get
            {
                if (_filters == null)
                {
                    return Builders<BsonDocument>.Filter.Empty.Render(
                        BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>(), 
                        BsonSerializer.SerializerRegistry
                    );
                }
                return _filters;
            }
        }

        private MongoDbQuery(string collectionName)
        {
            CollectionName = collectionName;
            _projection = new BsonDocument("_id", 0);
        }
        
        public string CollectionName { get; }

        public PageOptions PageOptions { get; set; }

        protected virtual IColumnFilterConverter<BsonDocument> QueryFilterConverter => new MongoDbColumnFilterConverter();

        public void AddColumn(QueryColumn queryColumn)
        {
            _projection.Add(new BsonDocument($"{queryColumn.Path}", 1));
        }

        private void AddColumns(IReadOnlyCollection<QueryColumn> queryColumns)
        {
            foreach (var queryColumn in queryColumns)
            {
                _projection.Add(new BsonDocument(new BsonDocument($"{queryColumn.Path}", 1)));
            }
        }

        private void AddFilters(IReadOnlyCollection<IColumnFilter> filters)
        {
            if (filters == null || !filters.Any())
            {
                return;
            }

            if (filters.Count > 1)
            {
                throw new NotImplementedException("more that 1 filter currently is not supported");
            }
            
            AddFilter(filters.Single());
        }

        private void AddFilter(IColumnFilter columnFilter)
        {
            _filters = QueryFilterConverter.Convert(columnFilter);;
        }

        public static MongoDbQuery CreateFromScheme(QueryScheme queryScheme)
        {
            var query = new MongoDbQuery(queryScheme.EntityName);
            query.AddColumns(queryScheme.Columns);
            query.PageOptions = queryScheme.PageOptions;
            query.AddFilters(queryScheme.Filters);
            return query;
        }
    }
}