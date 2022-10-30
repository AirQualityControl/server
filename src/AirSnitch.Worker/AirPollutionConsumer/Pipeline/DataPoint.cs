using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AirSnitch.Worker.AirPollutionConsumer.Pipeline
{
    /// <summary>
    ///ADT that represent a data point that contains a single unit of
    /// data from station ot provider
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// Information about station that sends the data
        /// </summary>
        [JsonProperty("station")]
        public StationInfo StationInfo { get; set; }

        /// <summary>
        /// Information about data provider or station
        /// </summary>
        [JsonProperty("dataProvider")]
        public DataProviderInfo DataProviderInfo { get; set; }

        /// <summary>
        /// A collection of measurements collected from station
        /// </summary>
        [JsonProperty("measurements")]
        public List<Measurement> Measurements { get; set; }

        /// <summary>
        /// Additional data that could be use to server
        /// </summary>
        [JsonProperty("additionalData")]
        public Dictionary<string, object> AdditionalData { get; set; }

        /// <summary>
        /// DateTime when measurements was collected
        /// </summary>
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Index value.Field is not mandatory.
        /// Some stations/providers might sent it while other is not
        /// </summary>
        [JsonProperty("index")]
        public AQIndexValue IndexValue { get; set; }
    }

    /// <summary>
    /// Abstract class that represent abstract air quality index
    /// </summary>
    public class AQIndexValue
    {
        /// <summary>
        /// Index value
        /// </summary>
        [JsonProperty("value")]
        public int IndexValue { get; set; }

        /// <summary>
        /// Human-readable index name
        /// </summary>
        [JsonProperty("type")]
        public string IndexName { get; set; }
    }

    public class Measurement
    {
        [JsonProperty("value")] public object Value { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }

    public class DataProviderInfo
    {
        [JsonProperty("id")] public string Tag { get; set; }
    }

    /// <summary>
    /// ADT that represent an information about station (AQ Controller) who sends a data
    /// </summary>
    public class StationInfo
    {
        private string _stationId;

        /// <summary>
        /// Unique station identifier
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        [JsonProperty("id")]
        public string StationId
        {
            get => _stationId;
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Station id could not be empty");
                }

                _stationId = value;
            }
        }

        /// <summary>
        /// Human readable station name
        /// </summary>
        [JsonProperty("name")]
        public string StationName { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        [JsonProperty("cityName")]
        public string CityName { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Geo-coordinates of the station
        /// </summary>
        [JsonProperty("geoCoordinates")]
        public GeoCoordinates GeoCoordinates { get; set; }
    }

    public struct GeoCoordinates
    {
        private const int LatitudeMinValue = 0;
        private const int LatitudeMaxValue = 90;
        private const int LongitudeMinValue = 0;
        private const int LongitudeMaxValue = 180;

        private double _longitudeValue;
        private double _latitudeValue;

        /// <summary>
        /// Longitude value of the point
        /// Value shout be in range between 0 and 90 degree
        /// </summary>
        [JsonProperty("long")]
        public double Longitude
        {
            get => _longitudeValue;
            set
            {
                ValidateLongitude(value);
                _longitudeValue = value;
            }
        }

        /// <summary>
        /// Latitude value of the point
        /// Value should be in range between 0 and 180
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude
        {
            get => _latitudeValue;
            set
            {
                ValidateLatitude(value);
                _latitudeValue = value;
            }
        }

        private void ValidateLatitude(double latitudeValue)
        {
            if (!(latitudeValue > LatitudeMinValue && latitudeValue < LatitudeMaxValue))
            {
                throw new ArgumentException(
                    $"Latitude value is invalid. Value should range from 0 to 90. Received value is {latitudeValue}");
            }
        }

        private void ValidateLongitude(double longitudeValue)
        {
            if (!(longitudeValue > LongitudeMinValue && longitudeValue < LongitudeMaxValue))
            {
                throw new ArgumentException(
                    $"Longitude value is invalid. Value should range from 0 to 180. Received value is {longitudeValue}");
            }
        }
    }
}