using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json.Serialization;
using AirSnitch.Api.Rest.Resources;
using AirSnitch.Api.Rest.ResponseBodyFormatters;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using Microsoft.AspNetCore.Http;

namespace AirSnitch.Api.Rest
{
    public class RestResponseBody : IResponseBody
    {
        private readonly HttpRequest _httpRequest;
        private readonly QueryResult _queryResult;
        private readonly IReadOnlyCollection<IApiResourceMetaInfo> _relatedResources;
        private readonly IReadOnlyCollection<IApiResourceMetaInfo> _requestedRelatedResources;


        public RestResponseBody(HttpRequest httpRequest, 
            QueryResult queryResult, 
            IReadOnlyCollection<IApiResourceMetaInfo> relatedResources, IReadOnlyCollection<IApiResourceMetaInfo> requestedRelatedResources = default)
        {
            _httpRequest = httpRequest;
            _queryResult = queryResult;
            _relatedResources = relatedResources;
            _requestedRelatedResources = requestedRelatedResources;
        }

        public string Value => Formatter.FormatResponse(this);
        public bool IsEmpty => !_queryResult.IsSuccess;

        protected virtual IResponseBodyFormatter Formatter =>
            new RestfullResponseBodyFormatter(_httpRequest, _queryResult, _relatedResources, _requestedRelatedResources);
        
        [JsonIgnore]
        public ContentType ContentType => new ContentType("application/json");
    }
}