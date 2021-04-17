using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models
{
    public class DataProviderDTO
    {
        /// <summary>
        /// User friendly name of data provider
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Url of original data provider web site
        /// </summary>
        [JsonProperty("web-site")]
        public Uri WebSiteUri { get; set; }

        /// <summary>
        /// Data update frequency interval
        /// </summary>
        [JsonProperty("dataUpdateInterval")]
        public TimeSpan DataUpdateInterval { get; set; }
    }
}
