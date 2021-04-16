using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirSnitch.API.Models
{
    public class GeoLocationDTO
    {
        /// <summary>
        /// Longitude value of the point
        /// Value shout be in range between 0 and 90 degree
        /// </summary>
        [JsonPropertyName("lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude value of the point
        /// Value should be in range between 0 and 180
        /// </summary>
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

    }
}
