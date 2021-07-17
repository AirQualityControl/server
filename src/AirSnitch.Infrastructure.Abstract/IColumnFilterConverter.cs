using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Infrastructure.Abstract
{
    public interface IColumnFilterConverter<out T>
    {
        T Convert(IColumnFilter columnFilter);
    }
}