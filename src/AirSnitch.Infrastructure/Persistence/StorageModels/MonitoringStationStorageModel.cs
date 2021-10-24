using System;
using AirSnitch.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class MonitoringStationStorageModel
    {
        public ObjectId PrimaryKey { get; internal set; }
        
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public LocationStorageModel Location { get; set; }

        public AirQualityIndexStorageModel AirQualityIndex { get; set; }

        public AirPollutionStorageModel AirPollution { get; set; }

        public DataProviderStorageModel DataProviderStorageModel { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<MonitoringStationStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.PrimaryKey).SetElementName("_id");
                cm.MapMember(cm => cm.Id).SetElementName("id");
                cm.MapMember(cm => cm.DisplayName).SetElementName("displayName");
                cm.MapMember(cm => cm.Location).SetElementName("location");
                cm.MapMember(cm => cm.AirQualityIndex).SetElementName("airQualityIndex");
                cm.MapMember(cm => cm.AirPollution).SetElementName("airPollution");
                cm.MapMember(cm => cm.DataProviderStorageModel).SetElementName("dataProvider");
            });
        }

        public MonitoringStation MapToDomainModel()
        {
            var station = new MonitoringStation();
            station.SetId(Id);
            station.SetName(DisplayName);
            station.SetAirPollution(BuildAirPollutionModel());
            station.SetLocation(BuildLocationModel());
            station.SetOwnerInfo(BuildOwnerInfo());

            return station;
        }

        private MonitoringStationOwner BuildOwnerInfo()
        {
           return DataProviderStorageModel.MapToDomainModel();
        }

        private Location BuildLocationModel()
        {
            return Location.MapToDomainModel();
        }

        private AirPollution BuildAirPollutionModel()
        {
            var airPollution = AirPollution.MapToDomainModel();
            airPollution.SetAirQualityIndexValue(AirQualityIndex.GetIndex(), AirQualityIndex.GetValue());
            return airPollution;
        }
    }
}