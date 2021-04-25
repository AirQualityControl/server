using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.Mongo.Mappers
{
    /// <summary>
    /// Mapper between storage model of type <see cref="UserStorageModel"/> model and mongoDb document
    /// </summary>
    internal static class UserMapper
    {
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<AirMonitoringStationSummaryDbModel>(cm =>
            {
                cm.MapMember(c => c.Address).SetElementName("address");
                cm.MapMember(c => c.CityName).SetElementName("cityName");
                cm.MapMember(c => c.StationId).SetElementName("stationId");
            });
            
            BsonClassMap.RegisterClassMap<UserStorageModel>(cm =>
            {
                cm.MapMember(c => c.FirstName).SetElementName("firstName");
                cm.MapMember(c => c.LastName).SetElementName("lastName");
                cm.MapMember(c => c.NickName).SetElementName("nickName");
                cm.MapMember(c => c.ClientUserId).SetElementName("clientUserId");
                cm.MapMember(c => c.LanguageCode).SetElementName("languageCode");
                cm.MapMember(c => c.IsBot).SetElementName("isBoot");
                cm.MapMember(c => c.IsActive).SetElementName("isActive");
                cm.MapMember(c => c.ClientInfo).SetElementName("clientInfo");
                cm.MapMember(c => c.AirMonitoringStationSummaryList).SetElementName("airMonitoringStations");
            });
        }
    }
}