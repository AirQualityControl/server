using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models
{
    public class GeoLocationDTO
    {
        /// <summary>
        /// Longitude value of the point
        /// Value shout be in range between 0 and 90 degree
        /// </summary>
        [JsonProperty("lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude value of the point
        /// Value should be in range between 0 and 180
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; set; }

    }
}
