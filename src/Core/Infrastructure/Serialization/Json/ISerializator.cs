namespace AirSnitch.Core.Infrastructure.Serialization
{
    public interface ISerializator<out TOut>
    {
        TOut Serialize(object objectToSerialize);
    }
}