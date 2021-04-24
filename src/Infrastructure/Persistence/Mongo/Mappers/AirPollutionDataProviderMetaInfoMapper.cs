using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.Mongo.Mappers
{
    /// <summary>
    /// Mapper between storage model of type <see cref="AirPollutionDataProviderMetaInfoStorageModel"/> model and mongoDb document
    /// </summary>
    internal static class AirPollutionDataProviderMetaInfoMapper
    {
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<AirPollutionDataProviderMetaInfoStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.Name).SetElementName("name");
                cm.MapMember(cm => cm.Tag).SetElementName("tag");
                cm.MapMember(cm => cm.WebSiteUri).SetElementName("webSiteUri");
                cm.MapMember(cm => cm.DataUpdateInterval).SetElementName("dataUpdateFrequency");
            });
        }
    }
}