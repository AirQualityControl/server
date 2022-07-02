using System;
using System.Collections.Generic;
using System.Linq;
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
        public readonly TransformBlock<Message, ValueTuple<Message, MonitoringStation>> Instance = new(async receivedMsg =>
        {
            var dataPoint = JsonConvert.DeserializeObject<DataPoint>(receivedMsg.Body);
            if (dataPoint == null)
            {
                throw new ArgumentException("");
            }

            var airMonitoringStation = new MonitoringStation
            {
                IsEmpty = false
            };
            airMonitoringStation.SetId(dataPoint.StationInfo.StationId);
            airMonitoringStation.SetName(dataPoint.StationInfo.StationName);
            airMonitoringStation.SetLocation(GetStationLocation());
            airMonitoringStation.SetAirPollution(GetAirPollution());
            
            await Task.Delay(300);
            return (receivedMsg, airMonitoringStation);
            
            AirPollution GetAirPollution()
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

            Location GetStationLocation()
            {
                var stationLocation = new Location();
                stationLocation.SetAddress(dataPoint.StationInfo.Address);
                stationLocation.SetCity(new City(name:dataPoint.StationInfo.CityName, "N/A"));
                stationLocation.SetAddress(dataPoint.StationInfo.Address);
                stationLocation.SetCountry(new Country(dataPoint.StationInfo.CountryCode));
                stationLocation.SetGeoCoordinates(new GeoCoordinates()
                {
                    Latitude = dataPoint.StationInfo.GeoCoordinates.Latitude,
                    Longitude = dataPoint.StationInfo.GeoCoordinates.Longitude
                });
                return stationLocation;
            }
        });
    }
}