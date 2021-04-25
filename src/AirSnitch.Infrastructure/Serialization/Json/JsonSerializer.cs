using Newtonsoft.Json;

namespace AirSnitch.Core.Infrastructure.Serialization.Json
{
    public class JsonSerializer : ISerializator<string>
    {
        public string Serialize(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}