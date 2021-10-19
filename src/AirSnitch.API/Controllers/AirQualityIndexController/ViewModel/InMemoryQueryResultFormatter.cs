using System.Collections.Generic;
using AirSnitch.Api.Extensions;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Controllers.AirQualityIndexController
{
    public class InMemoryQueryResultFormatter : IQueryResultFormatter
    {
        public IReadOnlyCollection<IQueryResultEntry> FormatResult(object result, ICollection<string> includedResources)
        {
            var resultEntries = new List<InMemoryQueryResultEntry>()
            {
                new InMemoryQueryResultEntry()
                {
                    ScalarValues = (Dictionary<string, object>)result,
                    IncludedValues = new Dictionary<string, object>(){}
                }
            };
            return resultEntries;
        }
    }
}