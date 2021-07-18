using System;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Filters;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.Filters;
using MongoDB.Bson;

namespace AirSnitch.Infrastructure.Persistence
{
    public class MongoDbQuery : IQuery
    {
        private readonly BsonDocument _projection;
        private BsonDocument _filters;

        //TODO: separate class
        public BsonDocument Projection => _projection;

        //TODO: separate class
        public BsonDocument Filter => _filters;

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
            throw new NotImplementedException();
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
            query.AddFilter(queryScheme.Filters.First());
            return query;
        }
    }
}