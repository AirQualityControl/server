using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Query
{
    public interface IQueryResultFormatter
    {
        IReadOnlyCollection<IQueryResultEntry> FormatResult(object result, ICollection<string> includedResources);
    }
}