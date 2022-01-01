using System;
using System.Linq;
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

        public void SeedData()
        {
            SeedApiUserCollection();
            SeedMonitoringStationCollection();
        }

        private void SeedMonitoringStationCollection()
        {
            var collection = _database.GetCollection<BsonDocument>("airMonitoringStation");
            for (int i = 1; i < 100; i++)
            {
                var monitoringStation = new BsonDocument()
                {
                    {"id", Guid.NewGuid().ToString()},
                    {"displayName", $"LUN_{Faker.RandomNumber.Next()}"},
                    {"location", new BsonDocument()
                        {
                            {"countryCode", "UA"},
                            {"city", new BsonDocument()
                                {
                                    {"code", "Kyiv"},
                                    {"name", "Kyiv"},
                                }
                            },
                            {"geoCoordinates", new BsonArray(){50.438686, 30.53331}},
                            {"address", $"{Faker.Address.StreetAddress()}"}
                        }
                    },
                    {"geoLocation", new BsonDocument()
                        {
                            {"type", "Point"},
                            {"coordinates", new BsonArray(){50.438686, 30.53331}},
                        }
                    },
                    {"airQualityIndex", new BsonDocument()
                        {
                            {"type", "US_AQI"},
                            {"value", Faker.RandomNumber.Next(3,500)},
                        }
                    },
                    {
                        "airPollution", new BsonDocument()
                        {
                            {"particles", new BsonArray()
                            {
                                new BsonDocument()
                                {
                                    {"name","PM2.5"},
                                    {"value", Faker.RandomNumber.Next(1,100)}
                                },
                                new BsonDocument()
                                {
                                    {"name","PM2.5"},
                                    {"value", Faker.RandomNumber.Next(1,50)}
                                }
                            }}
                        }
                    },
                    {"dataProvider", 
                        new BsonDocument()
                        {
                            {"id",Guid.NewGuid().ToString()},
                            {"name", "LUN Misto Air"},
                            {"web-site", "https://misto.lun.ua/air"}
                        }
                    }
                };
                
                collection.InsertOneAsync(monitoringStation);
            }
        }

        private void SeedApiUserCollection()
        {
            var collection = _database.GetCollection<BsonDocument>("apiUser");
            var supervisor = new BsonDocument()
            {
                { "id", "ccd8b4a9-22da-469b-8a1d-d447e07559a4" },
                { "firstName", "AirSnitch Supervisor" },
                { "lastName", "Supervisor" },
                { "email", "airsnitch@gmail.com" },
                { "profilePicUrl", "https://avatars.githubusercontent.com/u/11552351?v=4"},
                { "createdOn", "10/13/2021 15:28:45" },
                { "gender", "Male" },
                { "clients", new BsonArray()
                    {
                        new BsonDocument()
                        {
                            {"id", "16bc361c-30aa-4f4a-9056-6f3d1ef34f0e"},
                            {"name", "AirSnitchDevPanel"},
                            {"description", "Dev panel for airsnitch users"},
                            {"type", "PRODUCTION"},
                            {"createdOn", "10/13/2021 15:28:45"},
                            {"apiKey", new BsonDocument(){
                                {"value", "jTIDV7JPV/NxJhNtq+19ngtrmOC/+fACX7PtZUL4r0k="},
                                {"issueDate", "10/13/2021 15:28:45"},
                                {"expiryDate", "10/13/2022 15:28:45"},
                            }}
                        }
                    }
                },
                { "subscriptionPlan", new BsonDocument()
                    {
                        {"id", "5ed0055f8bf184fee385bb9e" },
                        { "name", "INTERNAL_APP" },
                        { "description", "Subscription plan for internal AirSnitch products" },
                        { "expirationDate", "12/31/9999 23:59:59" },
                    } 
                },
            };
            collection.InsertOneAsync(supervisor);

            for (int i = 1; i < 100; i++)
            {
                var apiUser = new BsonDocument()
                {
                    { "id", Guid.NewGuid().ToString() },
                    { "firstName", $"{Faker.Name.First()}" },
                    { "lastName", $"{Faker.Name.Last()}" },
                    { "email", $"{Faker.Internet.Email()}" },
                    { "profilePicUrl", $"{Faker.Internet.Url()}"},
                    { "createdOn", "10/13/2021 15:28:45" },
                    { "gender", "Male" },
                    { "clients", new BsonArray()
                        {
                            new BsonDocument()
                            {
                                {"id", Guid.NewGuid().ToString()},
                                {"name", $"{Faker.Company.Name()}"},
                                {"description", $"{Faker.Company.Name()}"},
                                {"type", "PRODUCTION"},
                                {"createdOn", "10/13/2021 15:28:45"},
                                {"apiKey", new BsonDocument(){
                                    {"value", "jVIQV7JPV/NxJhNtq-190gtrmOC/+fACX7PtZUL4r0k="},
                                    {"issueDate", "10/13/2021 15:28:45"},
                                    {"expiryDate", "10/13/2022 15:28:45"},
                                }}
                            }
                        }
                    },
                    { "subscriptionPlan", new BsonDocument()
                        {
                            {"id", "5ed0055f8bf184fee385bb9e" },
                            { "name", "INTERNAL_APP" },
                            { "description", "Subscription plan for internal AirSnitch products" },
                            { "expirationDate", "12/31/9999 23:59:59" },
                        } 
                    },
                };
                collection.InsertOneAsync(apiUser);
            }
        }
    }
}