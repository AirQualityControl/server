using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public class QueryResult
    {
        private readonly IReadOnlyCollection<IQueryResultEntry> _result;

        public QueryResult(IReadOnlyCollection<IQueryResultEntry> result, PageOptions pageOptions)
        {
            _result = result;
            PageOptions = pageOptions;
        }

        public bool IsSuccess => true;
        public IReadOnlyCollection<IQueryResultEntry> Value => _result;
        public PageOptions PageOptions { get; }
    }
}