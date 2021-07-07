using System;
using AirSnitch.Infrastructure.Abstract.Persistence;
using MongoDB.Bson;

namespace AirSnitch.Infrastructure.Persistence
{
    public class MongoDbQuery
    {
        private readonly BsonDocument _projection;

        private BsonDocument Projection => _projection;

        private BsonDocument Filter => new BsonDocument();
        
        public MongoDbQuery(string collectionName)
        {
            CollectionName = collectionName;
            _projection = new BsonDocument("_id", 0);
        }
        
        public void AddColumn(QueryColumn queryColumn)
        {
            _projection.Add(new BsonDocument($"{queryColumn.Path}", 1));
        }

        public void AddFilter(string filter)
        {
            //TODO:    
        }
        
        public string CollectionName { get; }
    }
}