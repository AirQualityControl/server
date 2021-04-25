using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models
{
    public class AirPollutionDTO
    {
        /// <summary>
        /// AQI value based on US EPA standard
        /// </summary>
        [JsonProperty("aqiusValue")]
        public int AqiusValue { get; set; }

        /// <summary>
        /// Property that indicate data time of current air pollution
        /// </summary>
        [JsonProperty("measurementDateTime")]
        public DateTime MeasurementDateTime { get; set; }

        /// <summary>
        /// Wind speed (m/s)
        /// </summary>
        [JsonProperty("windSpeed", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int WindSpeed { get; set; }

        /// <summary>
        /// humidity percentage value
        /// </summary>
        [JsonProperty("humidity", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Humidity { get; set; }

        /// <summary>
        /// Temperature value. By default in Celsius
        /// </summary>
        [JsonProperty("temperature", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Temperature { get; set; }

        /// <summary>
        /// Human oriented message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
