using AirSnitch.Infrastructure.Persistence.StorageModels.Mappers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence
{
    internal class MongoDbClientSettings
    {
        private IMongoDatabase _database;
        
        public MongoDbClientSettings InitConnection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database =  client.GetDatabase("AirQ");
            return this;
        }

        public IMongoDatabase Database => _database;
        
        public MongoDbClientSettings RegisterMap()
        {
            StorageModelsMapper.RegisterMapping();
            return this;
        }

        public MongoDbClientSettings PingDb()
        {
            _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
            return this;
        }
    }
}