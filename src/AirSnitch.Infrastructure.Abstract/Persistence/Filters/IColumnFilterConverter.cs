namespace AirSnitch.Infrastructure.Abstract.Persistence.Filters
{
    /// <summary>
    /// Interface that represent a contract for every column
    /// filter convert that be able to convert an abstract IColumnFilter to target storage type filter
    /// </summary>
    /// <typeparam name="T">Convertion result type</typeparam>
    public interface IColumnFilterConverter<out T>
    {
        T Convert(IColumnFilter columnFilter);
    }
}