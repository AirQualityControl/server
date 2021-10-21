using System;
using Microsoft.AspNetCore.Http;

namespace AirSnitch.Api.Rest.Links
{
    internal class BaseApiLink
    {
        private readonly string _scheme;
        private readonly string _hostValue;
        private readonly string _requestPath;

        private BaseApiLink(string scheme, string hostValue, string requestPath)
        {
            _scheme = scheme;
            _hostValue = hostValue;
            _requestPath = requestPath;
        }

        public Uri IncludePathValue => new Uri(new Uri($"{_scheme}://{_hostValue}"), _requestPath);
        public Uri Value => new Uri($"{_scheme}://{_hostValue}");
        
        public static BaseApiLink From(HttpRequest httpRequest)
        {
            return new BaseApiLink(
                scheme: httpRequest.Scheme,
                hostValue: httpRequest.Host.Value,
                requestPath: httpRequest.Path.Value
            );
        }
    }
}