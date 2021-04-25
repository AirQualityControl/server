using DeclarativeContracts.Functions;
using DeclarativeContracts.Precondition;
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
            Require.That(
                    element: owner,
                    predicate: Is.NotNull,
                    exceptionToThrow: new ArgumentNullException(nameof(owner))
                );
            Require.That(
                    element: roles,
                    predicate: Is.NotNull,
                    exceptionToThrow: new ArgumentNullException(nameof(roles))
                );
            Require.That(
                    element: key,
                    predicate: Is.NotNull,
                    exceptionToThrow: new ArgumentNullException(nameof(key))
                );

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
