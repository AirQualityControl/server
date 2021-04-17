using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Infrastructure.PathResolver
{
    public class Resourse
    {
        [JsonProperty("href")]
        public string Path { get; set; }
    }
}
