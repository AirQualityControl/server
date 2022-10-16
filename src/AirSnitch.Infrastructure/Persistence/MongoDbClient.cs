using System;
using AirSnitch.Infrastructure.Persistence.Serializers;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence
{
    public class MongoDbClient
    {
        private static readonly object Locker = new object();

        private readonly IMongoDatabase _instance;
        private MongoDbClient(MongoDbClientSettings settings)
        {
            _instance = settings.Database;
        }

        public static MongoDbClient Create(string connectionString, string dbName)
        {
            lock (Locker)
            {
                BsonSerializer.RegisterSerializer(typeof(GeoLocationStorageModel), new GeoLocationSerializer());
                
                var settings = new MongoDbClientSettings()
                    .InitConnection(connectionString, dbName)
                    .RegisterMap()
                    .PingDb();

                if (Boolean.TryParse(Environment.GetEnvironmentVariable("IsSeedData"), out bool _))
                {
                    settings.SeedData();
                }
                return new MongoDbClient(settings);
            }
        }

        public IMongoDatabase Db => _instance;
    }
}