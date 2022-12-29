using System;
using System.Globalization;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Persistence.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace AirSnitch.Infrastructure.Persistence.StorageModels
{
    internal class MonitoringStationStorageModel
    {
        public ObjectId PrimaryKey { get; set; }
        
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public LocationStorageModel Location { get; set; }

        public AirQualityIndexStorageModel AirQualityIndex { get; set; }

        public AirPollutionStorageModel AirPollution { get; set; }

        public DataProviderStorageModel DataProviderStorageModel { get; set; }

        public GeoLocationStorageModel GeoLocation { get; set; }

        public static void RegisterDbMap()
        {
            BsonClassMap.RegisterClassMap<MonitoringStationStorageModel>(cm =>
            {
                cm.MapMember(cm => cm.PrimaryKey).SetElementName("_id");
                cm.MapMember(cm => cm.Id).SetElementName("id");
                cm.MapMember(cm => cm.DisplayName).SetElementName("displayName");
                cm.MapMember(cm => cm.Location).SetElementName("location");
                cm.MapMember(cm => cm.GeoLocation).SetElementName("geoLocation")
                    .SetSerializer(new GeoLocationSerializer());
                cm.MapMember(cm => cm.AirQualityIndex).SetElementName("airQualityIndex");
                cm.MapMember(cm => cm.AirPollution).SetElementName("airPollution");
                cm.MapMember(cm => cm.DataProviderStorageModel).SetElementName("dataProvider");
            });
        }

        public MonitoringStation MapToDomainModel()
        {
            var station = new MonitoringStation(Id);
            station.SetName(DisplayName);
            station.SetAirPollution(BuildAirPollutionModel());
            station.SetLocation(BuildLocationModel());
            station.SetOwnerInfo(BuildOwnerInfo());
            station.PrimaryKey = PrimaryKey.ToString();
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
            var dateTimeToSet = !DateTime.TryParse(AirQualityIndex.DateTime, out DateTime dateTime) ? DateTime.MinValue : dateTime;
            var airPollution = AirPollution.MapToDomainModel();
            airPollution.SetAirQualityIndex(
                new UsaAirQualityIndex(),
                new UsaAiqIndexValue(
                    AirQualityIndex.Value,
                    dateTimeToSet));
            return airPollution;
        }

        public static MonitoringStationStorageModel CreateFromDomainModel(MonitoringStation monitoringStation)
        {
            var airPollution = monitoringStation.GetAirPollution();
            var location = monitoringStation.GetLocation();
            var geolocation = location.GeoCoordinates();
            var monitoringStationModel = new MonitoringStationStorageModel
            {
                Id = monitoringStation.Id,
                PrimaryKey = String.IsNullOrEmpty(monitoringStation.PrimaryKey) ? ObjectId.GenerateNewId() : ObjectId.Parse(monitoringStation.PrimaryKey),
                DisplayName = monitoringStation.DisplayName,
                Location = LocationStorageModel.MapFromDomainModel(location),
                GeoLocation = new GeoLocationStorageModel()
                {
                    Latitude = geolocation.Latitude,
                    Longitude = geolocation.Longitude
                },
                AirPollution = AirPollutionStorageModel.MapFromDomainModel(airPollution),
                AirQualityIndex = new AirQualityIndexStorageModel()
                {
                    TypeName = "US_AQI",
                    Value = airPollution.GetAirQualityIndexValue().NumericValue,
                    DateTime = airPollution.GetMeasurementsDateTime().ToString(CultureInfo.InvariantCulture),
                },
                DataProviderStorageModel = DataProviderStorageModel.MapFromDomainModel(monitoringStation.GetStationOwner()),
            };
            return monitoringStationModel;
        }
    }
}