using System;
using AirSnitch.Core.Infrastructure;
using AirSnitch.Core.Infrastructure.Configuration;
using AirSnitch.Infrastructure.Persistence.Exceptions;
using AirSnitch.Infrastructure.Persistence.Mongo.Mappers;
using AirSnitch.Infrastructure.Persistence.Mongo.Serializers;
using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence
{
    internal static class BaseMongoDbClient
    {
        private static  IMongoDatabase _dataBase;

        private static readonly IAppConfig AppConfig;

        static BaseMongoDbClient()
        {
            UserMapper.RegisterDbMap();
            CityMapper.RegisterDbMap();
            AirPollutionDataProviderMetaInfoMapper.RegisterDbMap();
            ClientInfoMapper.RegisterDbMap();
            RegisterDbMap();
            AppConfig = AppConfigurationFactory.GetAppConfig();
        }
        
        private static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<AirMonitoringStationStorageModel>(cm => 
            {
                cm.MapMember(c => c.Id).SetElementName("stationId");
                cm.MapMember(c => c.Name).SetElementName("name");
                cm.MapMember(c => c.LocalName).SetElementName("localName");
                cm.MapMember(c => c.IsActive).SetElementName("isActive");
                cm.MapMember(c => c.TimeZone).SetElementName("timeZone");
                cm.MapMember(c => c.City).SetElementName("city");
                cm.MapMember(c => c.GeoLocation).SetElementName("location").SetSerializer(
                    new GeoLocationSerializer());
                cm.MapMember(c => c.DataProviderMetaInfo).SetElementName("dataProviderMetaInfo");
            });
        }
        
        public static IMongoDatabase Db {
            get
            {
                //TODO: check method client.GetDatabase it should cache current DB(in theory)
                if (_dataBase == null)
                {
                    var connectionString = GetDbConnectionString();
                    var client = new MongoClient(connectionString);
                    _dataBase =  client.GetDatabase(AppConfig.Get("DB_NAME"));
                    return _dataBase;
                }
                return _dataBase;
            }
        }

        public static void PingDb()
        {
            PingDbInstance(Db);
        }

        private static void PingDbInstance(IMongoDatabase currentDatabase)
        {
            currentDatabase.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
        }

        private static string GetDbConnectionString()
        {
            return Boolean.Parse(AppConfig.Get("IS_DNS_SEED_LIST_CONNECTION")) 
                ? GetDnsSeedListConnectionString() : GetStandardConnectionString();
        }

        private static string GetStandardConnectionString()
        {
            MongoClientSettings settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(AppConfig.Get("DB_HOST"), GetDbPort()),
                RetryWrites = false,
                Credential =
                    MongoCredential.CreateCredential(AppConfig.Get("DB_NAME"),  
                        AppConfig.Get("DB_USERNAME"),  AppConfig.Get("DB_PASSWORD"))
            };
            return settings.ToString();
        }

        private static string GetDnsSeedListConnectionString()
        {
            string connectionString = $"mongodb+srv://" +
                                      $"{AppConfig.Get("DB_USERNAME")}:{AppConfig.Get("DB_PASSWORD")}" +
                                      $"@{AppConfig.Get("DB_ADDRESS")}/{AppConfig.Get("DB_NAME")}" +
                                      $"?retryWrites=true&w=majority";
            return connectionString;
        }

        private static int GetDbPort()
        {
            bool result = int.TryParse( AppConfig.Get("DB_PORT"), out int portNumber);

            if (!result)
            {
                throw new InvalidDatabasePortException("Port values is invalid. Please check config value [DB_PORT]");
            }

            return portNumber;
        }
    }
}