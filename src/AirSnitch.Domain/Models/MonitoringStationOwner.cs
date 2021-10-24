using System;

namespace AirSnitch.Domain.Models
{
    public class MonitoringStationOwner
    {
        private readonly string _id;
        private readonly string _name;
        private Uri _webSite;

        public MonitoringStationOwner(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public string Id => _id;
        
        public string Name => _name;

        public Uri WebSite => _webSite;

        public void SetWebSite(Uri webSiteUri)
        {
            _webSite = webSiteUri;
        }
    }
}