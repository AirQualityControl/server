using RestSharp;

namespace AirSnitch.Core.Infrastructure.Logging
{
    /// <summary>
    /// Interface that is suitable for HttpLogging
    /// </summary>
    public interface IHttpLogger
    {
        /// <summary>
        /// Log details about outgoing http requst
        /// </summary>
        /// <param name="httpRequest">HttpRequest object</param>
        /// <typeparam name="TRequest">Type of requested entity</typeparam>
        void LogHttpRequest<TRequest>(TRequest httpRequest) where TRequest : IRestRequest;

        /// <summary>
        /// Log detail of received Http response.
        /// </summary>
        /// <param name="httpResponse">Http response object</param>
        /// <typeparam name="TResponse">Response type</typeparam>
        void LogHttpResponse<TResponse>(TResponse httpResponse) where TResponse : IRestResponse;
    }
}