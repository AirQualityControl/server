using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirSnitch.API.Models
{
    public class AirPollutionDTO
    {
        /// <summary>
        /// AQI value based on US EPA standard
        /// </summary>
        [JsonPropertyName("aqiusValue")]
        public int AqiusValue { get; set; }

        /// <summary>
        /// Property that indicate data time of current air pollution
        /// </summary>
        [JsonPropertyName("measurementDateTime")]
        public DateTime MeasurementDateTime { get; set; }

        /// <summary>
        /// Wind speed (m/s)
        /// </summary>
        [JsonPropertyName("windSpeed")]
        public int WindSpeed { get; set; }

        /// <summary>
        /// humidity percentage value
        /// </summary>
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        /// <summary>
        /// Temperature value. By default in Celsius
        /// </summary>
        [JsonPropertyName("temperature")]
        public int Temperature { get; set; }

        /// <summary>
        /// Human oriented message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
