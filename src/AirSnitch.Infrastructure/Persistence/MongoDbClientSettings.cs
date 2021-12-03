using AirSnitch.Infrastructure.Persistence.StorageModels.Mappers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence
{
    internal class MongoDbClientSettings
    {
        private IMongoDatabase _database;
        
        public MongoDbClientSettings InitConnection(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _database =  client.GetDatabase(dbName);
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