using System;
using AirSnitch.Api.Controllers;
using Microsoft.AspNetCore.Http;

namespace AirSnitch.Api.Rest.Links
{
    public class LastLink : HalLink
    {
        private readonly HttpRequest _httpRequest;

        public LastLink(HttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }
        
        public override string Name => "last";

        public override string HrefValue {
            get
            {
                return new Uri(new Uri($"{_httpRequest.Scheme}://{_httpRequest.Host.Value}"), 
                    _httpRequest.Path.Value + _httpRequest.QueryString.Value).ToString();
            }
        }
    }
}