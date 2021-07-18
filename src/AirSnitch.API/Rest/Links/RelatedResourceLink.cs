using System;
using System.Linq;
using AirSnitch.Api.Controllers;
using AirSnitch.Infrastructure.Abstract.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AirSnitch.Api.Rest.Links
{
    public class RelatedResourceLink : HalLink
    {
        private readonly HttpRequest _httpRequest;
        private readonly QueryResult _queryResult;
        private readonly string _name;

        public RelatedResourceLink(HttpRequest httpRequest, QueryResult queryResult, string name)
        {
            _httpRequest = httpRequest;
            _queryResult = queryResult;
            _name = name;
        }
        
        public override string Name { get => _name; }
        
        public override string HrefValue {
            get
            {
                var baseUri = BaseApiLink.From(_httpRequest).Value;
                if (!_queryResult.IsScalar())
                {
                    var queryStringDictionary = _httpRequest.Query.ToDictionary(keySelector: k => k.Key, k => k.Value);
                    queryStringDictionary["includes"] = Name;
                    return $"{baseUri}{QueryString.Create(queryStringDictionary)}";
                }
                return $"{baseUri}/{_name}";
            }
        }
    }
}