using System;
using RestSharp;

namespace AirSnitch.Core.Infrastructure.Logging
{
    /// <summary>
    /// Concrete implementation of interface <see cref="IHttpLogger"/>
    /// </summary>
    public class HttpLogger : IHttpLogger
    {
        private readonly ILog _logger = new SerilogWrapper();
        
        public void LogHttpRequest<TRequest>(TRequest httpRequest) where TRequest : IRestRequest
        {
            _logger.Debug(new
            {
                TextMessage = "HTTP Request detail info",
                HttpRequestInfo = new
                {
                    RequestBody = httpRequest.Body,httpRequest,
                    Method = httpRequest.Method,
                    Parameters = String.Join(',', httpRequest.Parameters),
                    Resourse = httpRequest.Resource
                }
            });
        }

        public void LogHttpResponse<TResponse>(TResponse httpResponse) where TResponse : IRestResponse
        {
            _logger.Debug(new
            {
                TextMessage = "HTTP response detail info",
                HttpRequestInfo = new
                {
                    StatusCode = httpResponse.StatusCode,
                    ErrorMessage = httpResponse.ErrorMessage,
                    Body = httpResponse.Content
                }
            });
        }
    }
}