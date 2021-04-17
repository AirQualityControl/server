using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AirSnitch.Api.Infrastructure.PathResolver;

namespace AirSnitch.Api.Models.Responses
{
    public class Response
    {
        [JsonProperty("_links")]
        public Dictionary<string, Resourse> Links { get; set; }

        [JsonProperty("values")]
        public object Values { get; set; }

        [JsonProperty("includes")]
        public Dictionary<string, object> Includes { get; set; }
    }
}
