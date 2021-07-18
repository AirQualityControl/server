using System;
using System.Linq;
using AirSnitch.Api.Controllers;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AirSnitch.Api.Rest.Links
{
    public class PrevLink : HalLink
    {
        private readonly HttpRequest _httpRequest;
        private readonly PageOptions _pageOptions;

        public PrevLink(HttpRequest httpRequest, PageOptions pageOptions)
        {
            _httpRequest = httpRequest;
            _pageOptions = pageOptions;
        }
        
        public override string Name
        {
            get => "prev";
        }
        public override string HrefValue {
            get
            {
                var baseUri = BaseApiLink.From(_httpRequest).Value;
                if (_httpRequest.Query.ContainsKey(QueryParamName.Page) && _httpRequest.Query[QueryParamName.Page].Single() != "1")
                {
                    var queryStringDictionary = _httpRequest.Query.ToDictionary(keySelector: k => k.Key, k => k.Value);
                    var nextPageNumber = _pageOptions.PageNumber - 1;

                    queryStringDictionary["page"] = new StringValues(nextPageNumber.ToString());

                    return $"{baseUri}{QueryString.Create(queryStringDictionary)}";
                }
                return $"{baseUri}{_httpRequest.QueryString.Value}";
            }
        }
    }
}