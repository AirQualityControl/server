using System;
using AirSnitch.Core.Domain.Models;
using AirSnitch.Core.Infrastructure;
using AirSnitch.Infrastructure.Persistence.Mongo.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace AirSnitch.Infrastructure.Persistence
{
    public sealed class MongoDbComponent : ISystemComponent
    {
        public void CheckComponent()
        {
            PrepareDbConfiguration();
            CheckConnectivity();
            Console.WriteLine("MongoDB replica set is working properly!");
        }

        private void CheckConnectivity()
        {
            BaseMongoDbClient.PingDb();
        }

        private void PrepareDbConfiguration()
        {
            BsonSerializer.RegisterSerializer(typeof(GeoLocation), new GeoLocationSerializer());

            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camel case", pack, t => true);

            BaseMongoDbClient.PingDb();
        }
    }
}