using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models
{
    public class CityDTO
    {
        /// <summary>
        /// Friendly name of the city for end users
        /// </summary>
        [JsonProperty("friendlyName")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// City code.(Unique combination of letters)
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// State of particular city.For instance:
        /// city: Brovary
        /// state: Kyivska oblast
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Unique code of the country
        /// </summary>
        [JsonProperty("contryCode")]
        public string CountryCode { get; set; }
    }
}
