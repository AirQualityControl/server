using System;
using System.Linq;
using AirSnitch.Api.Controllers;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AirSnitch.Api.Rest.Links
{
    public class NextLink : HalLink
    {
        private readonly HttpRequest _httpRequest;
        private readonly PageOptions _pageOptions;

        public NextLink(HttpRequest httpRequest, PageOptions pageOptions)
        {
            _httpRequest = httpRequest;
            _pageOptions = pageOptions;
        }
        
        public override string Name => "next";

        public override string HrefValue {
            get
            {
                if (_httpRequest.Query.ContainsKey(QueryParamName.Page))
                {
                    var queryStringDictionary = _httpRequest.Query.ToDictionary(keySelector: k => k.Key, k => k.Value);
                    var nextPageNumber = _pageOptions.PageNumber + 1;

                    queryStringDictionary["page"] = new StringValues(nextPageNumber.ToString());
                    
                    return 
                        new Uri(
                            baseUri:new Uri($"{_httpRequest.Scheme}://{_httpRequest.Host.Value}"), 
                            relativeUri:_httpRequest.Path.Value + QueryString.Create(queryStringDictionary)
                        ).ToString();
                }
                return 
                    new Uri(
                        baseUri:new Uri($"{_httpRequest.Scheme}://{_httpRequest.Host.Value}"), 
                        relativeUri:_httpRequest.Path.Value + _httpRequest.QueryString.Value
                    ).ToString();
            }
        }
    }
}