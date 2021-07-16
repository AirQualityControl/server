using System;
using System.Linq;
using AirSnitch.Api.Controllers;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AirSnitch.Api.Rest.Links
{
    public class LastLink : HalLink
    {
        private readonly HttpRequest _httpRequest;
        private readonly PageOptions _pageOptions;
        public LastLink(HttpRequest httpRequest, PageOptions pageOptions)
        {
            _httpRequest = httpRequest;
            _pageOptions = pageOptions;
        }
        
        public override string Name => "last";

        public override string HrefValue {
            get
            {
                var queryStringDictionary = _httpRequest.Query.ToDictionary(keySelector: k => k.Key, k => k.Value);
                queryStringDictionary["page"] = new StringValues(_pageOptions.LastPageNumber.ToString());
                    
                return 
                    new Uri(
                        baseUri:new Uri($"{_httpRequest.Scheme}://{_httpRequest.Host.Value}"), 
                        relativeUri:_httpRequest.Path.Value + QueryString.Create(queryStringDictionary)
                    ).ToString();
            }
        }
    }
}