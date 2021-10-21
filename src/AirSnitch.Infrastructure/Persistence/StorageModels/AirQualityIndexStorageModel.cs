using AirSnitch.Domain.Models;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class AirQualityIndexStorageModel
    {
        public string TypeName { get; set; }

        public decimal Value { get; set; }
        
        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<AirQualityIndexStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.TypeName).SetElementName("type");
                cm.MapMember(cm => cm.Value).SetElementName("value");
            });
        }

        public IAirQualityIndexValue GetValue()
        {
            return new UsaAiqIndexValue(Value);
        }

        public IAirQualityIndex GetIndex()
        {
            return new UsaAirQualityIndex();
        }
    }
}