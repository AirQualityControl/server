namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public class QueryResult
    {
        private readonly object _result;

        public QueryResult(object result, PageOptions pageOptions)
        {
            _result = result;
            PageOptions = pageOptions;
        }

        public bool IsSuccess { get; set; }

        public PageOptions PageOptions { get; }
        
        public T GetResultAs<T>()
        {
            return (T) _result;
        }
    }
}