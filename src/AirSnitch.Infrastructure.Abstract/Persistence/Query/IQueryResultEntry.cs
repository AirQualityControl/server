using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Query
{
    public interface IQueryResultEntry
    {
        Dictionary<string, object> ScalarValues { get; }

        Dictionary<string, object> IncludedValues { get; }
    }
}