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

        public override bool Equals(object obj)
        {
            if (obj is Resourse resourse)
            {
                return resourse.Path.Equals(Path);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode()^4555;
        }
    }
}
