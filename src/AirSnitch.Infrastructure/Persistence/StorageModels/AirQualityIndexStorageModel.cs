using System;
using System.Globalization;
using AirSnitch.Domain.Models;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class AirQualityIndexStorageModel
    {
        public string TypeName { get; set; }

        public int Value { get; set; }

        public string DateTime { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<AirQualityIndexStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.TypeName).SetElementName("type");
                cm.MapMember(cm => cm.Value).SetElementName("value");
                cm.MapMember(cm => cm.DateTime).SetElementName("dateTime");
            });
        }

        public IAirQualityIndexValue GetValue()
        {
            return new UsaAiqIndexValue(Value, System.DateTime.Parse(DateTime, CultureInfo.InvariantCulture));
        }

        public IAirQualityIndex GetIndex()
        {
            return new UsaAirQualityIndex();
        }

        public static AirQualityIndexStorageModel MapFromDomainModel(IAirQualityIndexValue index)
        {
            return new AirQualityIndexStorageModel()
            {
                Value = index.NumericValue
            };
        }
    }
}