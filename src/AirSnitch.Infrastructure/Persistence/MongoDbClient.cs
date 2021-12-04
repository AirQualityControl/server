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
                var settings = new MongoDbClientSettings()
                    .InitConnection(connectionString, dbName)
                    .RegisterMap()
                    .PingDb();
                
                return new MongoDbClient(settings);
            }
        }

        public IMongoDatabase Db => _instance;
    }
}