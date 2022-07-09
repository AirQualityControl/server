using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.MessageQueue;
using AirSnitch.SDK;
using Newtonsoft.Json;
using City = AirSnitch.Domain.Models.City;
using GeoCoordinates = AirSnitch.Domain.Models.GeoCoordinates;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    public class ValidateMessageBlock
    {
        public ValidateMessageBlock()
        {
                
        }
        
        public TransformBlock<Message, ValueTuple<Message, MonitoringStation>> Instance => new TransformBlock<Message, ValueTuple<Message, MonitoringStation>>(Transform);

        private async Task<(Message, MonitoringStation)> Transform(Message receivedMsg)
        {
            var dataPoint = JsonConvert.DeserializeObject<DataPoint>(receivedMsg.Body);
            if (dataPoint == null)
            {
                throw new ArgumentException("");
            }

            var airMonitoringStation = new MonitoringStation { IsEmpty = false };
            airMonitoringStation.SetId(dataPoint.StationInfo.StationId);
            airMonitoringStation.SetName(dataPoint.StationInfo.StationName);
            airMonitoringStation.SetLocation(GetStationLocation(dataPoint));
            airMonitoringStation.SetAirPollution(GetAirPollution(dataPoint));

            await Task.Delay(300);
            return (receivedMsg, airMonitoringStation);
        }

        private AirPollution GetAirPollution(DataPoint? dataPoint)
        {
            var particlesCollection = new List<IAirPollutionParticle>(dataPoint.Measurements.Count);

            foreach (var measurement in dataPoint.Measurements)
            {
                //particlesCollection.Add(ne);
            }

            var airPollution = new AirPollution(null);
            airPollution.SetAirQualityIndexValue(null, null);
            return airPollution;
        }

        private Location GetStationLocation(DataPoint? dataPoint)
        {
            var stationLocation = new Location();
            stationLocation.SetAddress(dataPoint.StationInfo.Address);
            stationLocation.SetCity(new City(name: dataPoint.StationInfo.CityName, "N/A"));
            stationLocation.SetAddress(dataPoint.StationInfo.Address);
            stationLocation.SetCountry(new Country(dataPoint.StationInfo.CountryCode));
            stationLocation.SetGeoCoordinates(new GeoCoordinates() { Latitude = dataPoint.StationInfo.GeoCoordinates.Latitude, Longitude = dataPoint.StationInfo.GeoCoordinates.Longitude });
            return stationLocation;
        }
    }
}