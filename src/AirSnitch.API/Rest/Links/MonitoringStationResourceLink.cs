using System;
using Microsoft.AspNetCore.Http;

namespace AirSnitch.Api.Rest.Links
{
    public class MonitoringStationResourceLink
    {
        private readonly HttpRequest _request;
        private readonly string _id;

        public MonitoringStationResourceLink(HttpRequest request, string id = null)
        {
            _request = request;
            _id = id;
        }

        public String Value => $"{BaseApiLink.From(_request).Value}/monitoringStation/{_id}";
    }
}