using AirSnitch.Core.Infrastructure.Serialization;

namespace AirSnitch.Core.Infrastructure.Network.HTTP
{
    /// <summary>
    /// Class that represent content type in standard JSON format
    /// </summary>
    public class ApplicationJsonContentType : IContentType
    {
        public string StringRepresentation => "application/json";

        //TODO: ado[t to JSON serializer injections
        public ISerializator<string> Serializator => null; //new Serialization.Json.JsonSerializer();
    }
}