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


        public RestResponseBody(HttpRequest httpRequest, 
            QueryResult queryResult, 
            IReadOnlyCollection<IApiResourceMetaInfo> relatedResources)
        {
            _httpRequest = httpRequest;
            _queryResult = queryResult;
            _relatedResources = relatedResources;
        }

        public string Value => Formatter.FormatResponse(this);
        
        protected virtual IResponseBodyFormatter Formatter =>
            new RestfullResponseBodyFormatter(_httpRequest, _queryResult, _relatedResources);
        
        [JsonIgnore]
        public ContentType ContentType => new ContentType("application/json");
    }
}