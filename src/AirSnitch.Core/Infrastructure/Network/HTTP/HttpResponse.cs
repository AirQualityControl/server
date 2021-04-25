using System.Net;

namespace AirSnitch.Core.Infrastructure.Network.HTTP
{
    /// <summary>
    /// Http response received after performing request
    /// </summary>
    /// <typeparam name="T">Type that represent desirable response</typeparam>
    public class HttpResponse<T>
    {
        /// <summary>
        /// Body of http response
        /// </summary>
        public T Body { get; set; }
        /// <summary>
        /// Marker that define is request is success or not
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Status code of current request
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        
        /// <summary>
        /// Property that represent unsuccessful Http response.
        /// Could be returned if requested service return unsuccessful status code
        /// or completely unavailable
        /// </summary>
        public static HttpResponse<T> Error { get; } = new HttpResponse<T>{IsSuccess = false};
    }
}
