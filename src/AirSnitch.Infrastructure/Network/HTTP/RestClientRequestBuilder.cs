using System.Collections.Generic;
using System.Diagnostics.Contracts;
using AirSnitch.Core.Infrastructure.Network.HTTP;


using RestSharp;

namespace AirSnitch.Infrastructure.Network.HTTP
{
    /// <summary>
    /// Class that build Http Request.
    /// </summary>
    internal class RestClientRequestBuilder
    {
        /// <summary>
        /// Build simple get request to specifies HttpResource
        /// </summary>
        /// <param name="httpResource">Requested resource
        /// Precondition:
        ///     httpResource is not null
        /// </param>
        /// <returns>RestRequest result request</returns>
        public RestRequest BuildRequest(IHttpResource httpResource)
        {
            Contract.Requires(httpResource != null);
            
            return CreateRestRequest(httpResource);
        } 
        /// <summary>
        /// Build get request for specified http resource with parameters collection
        /// </summary>
        /// <param name="httpResource">Requested resource
        /// Precondition:
        ///     httpResource is not null;
        ///     requestParameters has at least one element
        /// </param>
        /// <returns>RestRequest result request</returns>
        public RestRequest BuildRequest(IHttpResource httpResource, IReadOnlyCollection<KeyValuePair<string, string>> requestParameters)
        {
            Contract.Requires(httpResource != null);
            Contract.Requires(requestParameters.Count > 0);
            
            var request = CreateRestRequest(httpResource);
            AddParamsToRequest(request, requestParameters);
            
            return request;
        }

        public RestRequest BuildPostRequest<TModel>(IHttpResource resource, RequestBody<TModel> requestBody)
        {
            var restRequest = new RestRequest(resource.Name) {Method = Method.POST};

            restRequest.AddParameter(
                requestBody.ContentType.StringRepresentation,
                requestBody.Serialize(),
                ParameterType.RequestBody);
            
            restRequest.AddHeader("Content-Type", requestBody.ContentType.StringRepresentation);
            
            return restRequest;
        }

        private RestRequest CreateRestRequest(IHttpResource httpResource)
        {
            var dataFormat = ConvertToRestSharpDataFormat(httpResource.Format);
            
            var request = new RestRequest($"/{httpResource.Name}", dataFormat);

            return request;
        }

        private void AddParamsToRequest(RestRequest request, IReadOnlyCollection<KeyValuePair<string, string>> requestParameters)
        {
            foreach (var param in requestParameters) 
            {
                request.AddQueryParameter(param.Key, param.Value);
            }
        }

        private static DataFormat ConvertToRestSharpDataFormat(HttpResourceFormat httpResourceFormat)
        {
            var restSharpDataFormat = httpResourceFormat switch
            {
                HttpResourceFormat.Json => DataFormat.Json,
                HttpResourceFormat.Xml => DataFormat.Xml
            };
            return restSharpDataFormat;
        }
    }
}