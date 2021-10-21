using System;

namespace AirSnitch.Domain.Models
{
    public class MonitoringStationOwner
    {
        private readonly string _name;
        private readonly Uri _webSite;

        public MonitoringStationOwner(string name, Uri webSite = default)
        {
            _name = name;
            _webSite = webSite;
        }
           
        public string Name => _name;

        public Uri WebSite => _webSite;
    }
}