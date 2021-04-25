using AirSnitch.Core.Domain.Models;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.Mongo.Mappers
{
    internal static class CityMapper
    {
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<City>(cm =>
            {
                cm.MapMember(cm => cm.Code).SetElementName("code");
                cm.MapMember(cm => cm.State).SetElementName("state");
                cm.MapMember(cm => cm.CountryCode).SetElementName("country");
                cm.MapMember(cm => cm.FriendlyName).SetElementName("friendlyName");
            });
        }
    }
}