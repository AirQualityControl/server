using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirSnitch.API.Models
{
    public class CityDTO
    {
        /// <summary>
        /// Friendly name of the city for end users
        /// </summary>
        [JsonPropertyName("friendlyName")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// City code.(Unique combination of letters)
        /// </summary>
        [JsonPropertyName("friendlyName")]
        public string Code { get; set; }

        /// <summary>
        /// State of particular city.For instance:
        /// city: Brovary
        /// state: Kyivska oblast
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// Unique code of the country
        /// </summary>
        [JsonPropertyName("contryCode")]
        public string CountryCode { get; set; }
    }
}
