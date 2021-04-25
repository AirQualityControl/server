using System;
using System.Threading.Tasks;
using AirSnitch.Core.Infrastructure;
using AirSnitch.Core.Infrastructure.Configuration;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Jobs
{
    public class HangfireJobStoreComponent : ISystemComponent
    {
        private readonly IAppConfig _appConfig;
        
        public HangfireJobStoreComponent()
        {
            _appConfig = AppConfigurationFactory.GetAppConfig();
        }
        
        public void CheckComponent()
        {
            PrepareAndRunJobStore();
        }

        private void PrepareAndRunJobStore()
        {
            var options = new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new DropMongoMigrationStrategy(),
                    BackupStrategy = new NoneMongoBackupStrategy()
                },
                MigrationLockTimeout = TimeSpan.FromMinutes(2)
            };
            
            var appConfig = AppConfigurationFactory.GetAppConfig();
            
            var mongoDbSettings = MongoClientSettings.FromConnectionString(GetDnsSeedListConnectionString());
            mongoDbSettings.MaxConnectionIdleTime = TimeSpan.FromMinutes(3);
            mongoDbSettings.ConnectTimeout = TimeSpan.FromSeconds(120);
            mongoDbSettings.SocketTimeout = TimeSpan.FromSeconds(120);

            var mongoStorage = new MongoStorage(
                mongoDbSettings, appConfig.Get("DB_NAME"), options);
            
            JobStorage.Current = mongoStorage;
            
            var a = new BackgroundJobServer();
            
            
            Task.Run(async () =>
            {
                using(new BackgroundJobServer(mongoStorage))
                {
                    Console.WriteLine("Hangfire Server up and running!");
                    await Task.Delay(Int32.MaxValue);
                }
            });
        }

        protected virtual BackgroundJobServerOptions GetBackgroundJobServerOptions()
        {
            return new BackgroundJobServerOptions();
        }

        private  string GetDnsSeedListConnectionString()
        {
            string connectionString = $"mongodb+srv://" +
                                      $"{_appConfig.Get("DB_USERNAME")}:{_appConfig.Get("DB_PASSWORD")}" +
                                      $"@{_appConfig.Get("DB_ADDRESS")}/{_appConfig.Get("DB_NAME")}" +
                                      $"?retryWrites=true&w=majority";
            return connectionString;
        }
    }
}