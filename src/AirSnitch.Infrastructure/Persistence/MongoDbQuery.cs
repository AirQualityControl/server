using System;
using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence;
using MongoDB.Bson;

namespace AirSnitch.Infrastructure.Persistence
{
    public class MongoDbQuery : IQuery
    {
        private readonly BsonDocument _projection;

        public BsonDocument Projection => _projection;

        public BsonDocument Filter => new BsonDocument();

        private MongoDbQuery(string collectionName)
        {
            CollectionName = collectionName;
            _projection = new BsonDocument("_id", 0);
        }
        
        public string CollectionName { get; }

        public PageOptions PageOptions { get; set; }

        public void AddColumn(QueryColumn queryColumn)
        {
            _projection.Add(new BsonDocument($"{queryColumn.Path}", 1));
        }

        public void AddColumns(IReadOnlyCollection<QueryColumn> queryColumns)
        {
            foreach (var queryColumn in queryColumns)
            {
                _projection.Add(new BsonDocument(new BsonDocument($"{queryColumn.Path}", 1)));
            }
        }

        public void AddFilter(string filter)
        {
            //TODO:    
        }
        
        public static MongoDbQuery CreateFromScheme(QueryScheme queryScheme)
        {
            var query = new MongoDbQuery(queryScheme.EntityName);
            query.AddColumns(queryScheme.Columns);
            query.PageOptions = queryScheme.PageOptions;
            return query;
        }
    }
}