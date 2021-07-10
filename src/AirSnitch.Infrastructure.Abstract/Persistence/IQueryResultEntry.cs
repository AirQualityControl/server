using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public interface IQueryResultEntry
    {
        Dictionary<string, object> ScalarValues { get; }

        Dictionary<string, object> IncludedValues { get; }
    }
}