namespace AirSnitch.Core.Infrastructure.Network.HTTP
{
    /// <summary>
    /// Class that represent a body of request
    /// </summary>
    public class RequestBody<T> 
    {
        /// <summary>
        /// Actual value of request body that will be sent in request
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Content type
        /// </summary>
        public IContentType ContentType { get; set; }

        /// <summary>
        /// Serialize body of the request to string, using specific content type serializer
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            var serializator = ContentType.Serializator;
            return serializator.Serialize(Value);
        }
    }
}