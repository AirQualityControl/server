using System.Collections.Generic;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Extensions
{
    public class InMemoryQueryResultEntry : IQueryResultEntry
    {
        public Dictionary<string, object> ScalarValues { get; set; }
        public Dictionary<string, object> IncludedValues { get; set; }
    }
}