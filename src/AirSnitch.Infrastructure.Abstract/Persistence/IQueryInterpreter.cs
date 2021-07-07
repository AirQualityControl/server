namespace AirSnitch.Infrastructure.Abstract.Persistence
{
    public interface IQueryInterpreter<out TValue>
    {
        TValue InterpretQuery(QueryScheme queryScheme);
    }
}