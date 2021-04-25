using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using AirSnitch.Core.Infrastructure.Network.HTTP;
using AirSnitch.Core.Infrastructure.Network.HTTP.Authentication;


using RestSharp;

namespace AirSnitch.Infrastructure.Network.HTTP
{
    /// <summary>
    /// Class that represent 
    /// </summary>
    public static class Http
    {
        //private static readonly IHttpLogger _httpLogger = LogManager.GetHttpLogger();
        
        /// <summary>
        ///    Perform HTTP GET request 
        /// </summary>
        /// <param name="baseAddress">Base address of request</param>
        /// <param name="httpResource">Http resource to query</param>
        /// <typeparam name="Target">HttpResponse with body of Target type</typeparam>
        /// <returns></returns>
        public static async Task<HttpResponse<Target>> Get<Target>(
            Uri baseAddress, 
            IHttpResource httpResource) where Target : new() 
        {
            Contract.Requires(baseAddress != null);
            Contract.Requires(httpResource != null);

            return await InternalGetRequest<Target>(baseAddress, httpResource);
        }
        
        /// <summary>
        ///    Perform HTTP GET request 
        /// </summary>
        /// <param name="baseAddress">Base address of request</param>
        /// <param name="httpResource">Http resource to query</param>
        /// <param name="requestParameters">Parameters of Get request</param>
        /// <typeparam name="Target">HttpResponse with body of Target type</typeparam>
        /// <returns></returns>
        public static async Task<HttpResponse<Target>> Get<Target>(
            Uri baseAddress, 
            IHttpResource httpResource, 
            IReadOnlyCollection<KeyValuePair<string, string>> requestParameters) where Target : new()
        {
            Contract.Requires(baseAddress != null);
            Contract.Requires(httpResource != null);
            Contract.Requires(requestParameters.Count > 0);

            return await InternalGetRequest<Target>(baseAddress, httpResource, requestParameters);
        }
        
       /// <summary>
       ///     Perform HTTP Post asynchronously
       /// </summary>
       /// <param name="baseAddress">Base of POST request destination</param>
       /// <param name="resource">Destination http resource</param>
       /// <param name="body">Request body with TModel that will be sent to the server via POST request.</param>
       /// <param name="authScheme">Auth scheme that will be used to authenticate request</param>
       /// <typeparam name="TModel">Model type</typeparam>
       /// <typeparam name="TResult">Result of execution that server will return</typeparam>
       /// <returns></returns>
        public static async Task<HttpResponse<TResult>> Post<TModel, TResult>(
                Uri baseAddress,
                IHttpResource resource,
                RequestBody<TModel> body,
                AuthScheme authScheme
            )
            where TResult : new()
        {
            Contract.Requires(baseAddress != null);
            Contract.Requires(resource != null);
            Contract.Requires(authScheme != null);

            return await InternalPostRequest<TModel, TResult>(baseAddress, resource, body, authScheme);
        }
        
       private static async Task<HttpResponse<TResult>> InternalPostRequest<TModel, TResult>(
           Uri baseAddress,
           IHttpResource resource,
           RequestBody<TModel> body,
           AuthScheme authScheme
       )
           where TResult : new()
       {
           var client = new RestClient(baseAddress);
           client.Authenticator = authScheme.GetAuthenticator();
           var restRequest = new RestClientRequestBuilder().BuildPostRequest(resource, body);
           
           //_httpLogger.LogHttpRequest(restRequest);
           
           var result = await client.ExecuteAsync<TResult>(restRequest);

           //_httpLogger.LogHttpResponse(result);
           
           if (result.IsSuccessful)
           {
               return new HttpResponse<TResult>
               {
                   IsSuccess = true,
                   StatusCode = result.StatusCode,
                   Body = result.Data
               };
           }

           return HttpResponse<TResult>.Error;
       }

        private static async Task<HttpResponse<Target>> InternalGetRequest<Target>(
            Uri baseAddress, 
            IHttpResource httpResource, 
            IReadOnlyCollection<KeyValuePair<string, string>> requestParameters = default) where Target : new()
        {
            var client = new RestClient(baseAddress.ToString());
            var request = new RestClientRequestBuilder().BuildRequest(httpResource, requestParameters);

            //_httpLogger.LogHttpRequest(request);
            
            var result = await client.ExecuteAsync<Target>(request);
            
            //_httpLogger.LogHttpResponse(result);
            
            if (result.IsSuccessful)
            {
                return new HttpResponse<Target>()
                {
                    IsSuccess = true,
                    StatusCode = result.StatusCode,
                    Body = result.Data
                };
            }
            return  HttpResponse<Target>.Error;
        }
    }
}
