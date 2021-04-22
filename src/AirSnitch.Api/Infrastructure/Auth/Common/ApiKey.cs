using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace AirSnitch.Api.Models.Internal
{
    public class ApiKey
    {
        public ApiKey(int id, string owner, string key, DateTime created, List<string> roles)
        {
            Contract.Requires<ArgumentNullException>(owner != null, nameof(owner));
            Contract.Requires<ArgumentNullException>(key != null, nameof(key));
            Contract.Requires<ArgumentNullException>(roles != null, nameof(roles));

            Id = id;
            Owner = owner;
            Key = key;
            CreatedOn = created;
            Roles = roles;
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonIgnore]
        public List<string> Roles { get; set; }
    }
}
