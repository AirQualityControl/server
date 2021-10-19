using System;
using System.Collections;
using System.Collections.Generic;
using AirSnitch.Api.Extensions;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Api.Controllers.AirQualityIndexController.ViewModel
{
    public class AirQualityIndexResponseFormatter : IQueryResultFormatter
    {
        public IReadOnlyCollection<IQueryResultEntry> FormatResult(object result, ICollection<string> includedResources)
        {
            var typedResult = (List<Dictionary<string, object>>) result;
            
            var resultEntries = new List<InMemoryQueryResultEntry>()
            {
                new InMemoryQueryResultEntry()
                {
                    ScalarValues = typedResult[0],
                    IncludedValues = new Dictionary<string, object>(){}
                }
            };
            return resultEntries;
        }
    }
}