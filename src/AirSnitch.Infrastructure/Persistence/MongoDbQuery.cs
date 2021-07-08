using System;
using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence;
using MongoDB.Bson;

namespace AirSnitch.Infrastructure.Persistence
{
    public class MongoDbQuery : IQuery
    {
        private readonly BsonDocument _projection;

        private BsonDocument Projection => _projection;

        private BsonDocument Filter => new BsonDocument();

        private int _itemsToSkip;
        private int _itemsLimit;
        
        private MongoDbQuery(string collectionName)
        {
            CollectionName = collectionName;
            _projection = new BsonDocument("_id", 0);
        }
        
        public string CollectionName { get; }

        public int ItemsToSkip => _itemsToSkip;

        public int ItemsLimit => _itemsLimit;

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

        public void AddPageOptions(PageOptions pageOptions)
        {
            _itemsToSkip = pageOptions.PageNumber * pageOptions.ItemsPerPage;
            _itemsLimit = pageOptions.ItemsPerPage;
        }

        public void AddFilter(string filter)
        {
            //TODO:    
        }
        
        public static MongoDbQuery CreateFromScheme(QueryScheme queryScheme)
        {
            var query = new MongoDbQuery(queryScheme.EntityName);
            query.AddColumns(queryScheme.Columns);
            query.AddPageOptions(queryScheme.PageOptions);
            return query;
        }

    }
}