using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract
{
    public class EqualColumnFilter : IColumnFilter
    {
        public EqualColumnFilter(QueryColumn column, object value)
        {
            Column = column;
            Value = value;
        }
        
        public QueryColumn Column { get; }
        public object Value { get; }

        public ComparisonOperand Operand => ComparisonOperand.Equal;
    }
}