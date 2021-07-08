namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public class QueryResult
    {
        private readonly object _result;

        public QueryResult(object result)
        {
            _result = result;
        }
        
        public T GetResultAs<T>()
        {
            return (T) _result;
        }
    }
}