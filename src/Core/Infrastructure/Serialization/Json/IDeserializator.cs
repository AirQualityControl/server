namespace AirSnitch.Core.Infrastructure.Serialization
{
    public interface IDeserializator<in TIn , out TOut>
    {
        TOut Deserialize(TIn entityToDesearialize);
    }
}