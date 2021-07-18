using AirSnitch.Infrastructure.Abstract.Persistence.Query;

namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public interface IColumnFilter
    {
        QueryColumn Column { get; }
        object Value { get; }
        
        ComparisonOperand Operand { get; }
    }

    
    
    
    public enum ComparisonOperand
    {
        Equal,
    }
}