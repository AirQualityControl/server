using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AirSnitch.API.Models
{
    public class DataProviderDTO
    {
        /// <summary>
        /// User friendly name of data provider
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Url of original data provider web site
        /// </summary>
        [JsonPropertyName("uri")]
        public Uri WebSiteUri { get; set; }

        /// <summary>
        /// Data update frequency interval
        /// </summary>
        [JsonPropertyName("dataUpdateInterval")]
        public TimeSpan DataUpdateInterval { get; set; }
    }
}
