using AirSnitch.Api.Infrastructure.PathResolver;
using AirSnitch.Api.Infrastructure.PathResolver.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Responses
{
    public class PaginativeResponse
    {
        [JsonProperty("_links")]
        public Dictionary<string, Resourse> Links { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("items")]
        public List<Response> Responses { get; set; }
    }
}
