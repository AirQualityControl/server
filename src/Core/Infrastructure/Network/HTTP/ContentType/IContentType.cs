using AirSnitch.Core.Infrastructure.Serialization;

namespace AirSnitch.Core.Infrastructure.Network.HTTP
{
    /// <summary>
    /// The Content-Type entity header is used to indicate the media type of the resource.
    /// </summary>
    public interface IContentType
    {
        /// <summary>
        /// String representation of content type value that will be used in request headers.
        /// For instance: application/json, text/plain;
        /// </summary>
        string StringRepresentation { get; }
        
        /// <summary>
        /// Serializer of content type that serialize request content(body) to string.
        /// </summary>
        ISerializator<string> Serializator { get; }
    }
}