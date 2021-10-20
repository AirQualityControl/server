using System;
using System.Collections;
using System.Collections.Generic;

namespace AirSnitch.Infrastructure.Abstract.Persistence.Query
{
    public class QueryResult
    {
        private readonly object _result;
        private readonly IQueryResultFormatter _queryResultFormatter;

        public QueryResult(
            object resultData,
            IQueryResultFormatter queryResultFormatter,
            PageOptions pageOptions = default)
        {
            _result = resultData;
            _queryResultFormatter = queryResultFormatter;
            PageOptions = pageOptions;
        }

        public bool IsSuccess => true;
        
        public IReadOnlyCollection<IQueryResultEntry> GetFormattedValue(ICollection<string> includedResources)
        {
            return _queryResultFormatter.FormatResult(_result, includedResources);
        }

        public PageOptions PageOptions { get; }

        public bool IsScalar()
        {
            var collectionResult = _result as ICollection;

            if (collectionResult == null)
            {
                throw new Exception();
            }

            if (collectionResult.Count > 1)
            { 
                return false;
            }
            return true;
        }
    }
}