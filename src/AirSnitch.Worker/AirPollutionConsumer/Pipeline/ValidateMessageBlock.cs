using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.MessageQueue;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using City = AirSnitch.Domain.Models.City;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class ValidateMessageBlock
    {
        private readonly ILogger<AirPollutionDataConsumer> _logger;
        public ValidateMessageBlock(ILogger<AirPollutionDataConsumer> logger)
        {
            _logger = logger;
        }
        public TransformBlock<Message, ValueTuple<Message, MonitoringStation>> Instance => new TransformBlock<Message, ValueTuple<Message, MonitoringStation>>(Transform);

        private async Task<(Message, MonitoringStation)> Transform(Message receivedMsg)
        {
            var airMonitoringStation = new MonitoringStation() { IsEmpty = false };
            try
            {
                var dataPoint = JsonConvert.DeserializeObject<DataPoint>(receivedMsg.Body);
                if (dataPoint == null)
                {
                    throw new ArgumentException("Deserialized object is null");
                }
                airMonitoringStation.SetName(dataPoint.StationInfo?.StationName);
                airMonitoringStation.SetLocation(GetStationLocation(dataPoint));
                airMonitoringStation.SetAirPollution(GetAirPollution(dataPoint));
                var stationOwner = new MonitoringStationOwner(dataPoint.DataProviderInfo?.Tag, dataPoint.DataProviderInfo?.Name);
                stationOwner.SetWebSite(new Uri(dataPoint.DataProviderInfo?.Uri));
                airMonitoringStation.SetOwnerInfo(stationOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex.Message}, {ex.StackTrace}");
            }
            return (receivedMsg, airMonitoringStation);
            
        }

        private AirPollution GetAirPollution(DataPoint dataPoint)
        {
            var particlesCollection = new List<IAirPollutionParticle>(dataPoint.Measurements.Count);

            foreach (var measurement in dataPoint.Measurements)
            {
                switch (measurement.Name)
                {
                    case "PM10":
                        particlesCollection.Add(new Pm10Particle(Convert.ToDouble(measurement.Value)));
                        break;
                    case "PM25":
                        particlesCollection.Add(new Pm25Particle(Convert.ToDouble(measurement.Value)));
                        break;
                }
            }
            
            var airPollution = new AirPollution(particlesCollection, dataPoint.DateTime);
            airPollution.SetAirQualityIndex(new UsaAirQualityIndex(), new UsaAiqIndexValue(dataPoint.IndexValue.IndexValue, dataPoint.DateTime));
            return airPollution;
        }

        private Location GetStationLocation(DataPoint dataPoint)
        {
            var stationLocation = new Location();
            stationLocation.SetAddress(dataPoint.StationInfo.Address);
            stationLocation.SetCity(new City(name: dataPoint.StationInfo.CityName));
            stationLocation.SetAddress(dataPoint.StationInfo.Address);
            stationLocation.SetCountry(new Country(dataPoint.StationInfo.CountryCode));
            stationLocation.SetGeoCoordinates(
                new AirSnitch.Domain.Models.GeoCoordinates() 
                { 
                    Latitude = dataPoint.StationInfo.GeoCoordinates.Latitude, 
                    Longitude = dataPoint.StationInfo.GeoCoordinates.Longitude 
                }
            );
            return stationLocation;
        }
    }
}