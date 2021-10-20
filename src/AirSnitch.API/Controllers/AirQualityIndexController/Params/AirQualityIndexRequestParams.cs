using AirSnitch.Domain.Models;
using AirSnitch.Infrastructure.Abstract.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AirSnitch.Api.Controllers.AirQualityIndexController
{
    public class AirQualityIndexRequestParams
    {
        [FromQuery(Name = "long")] 
        public double LongitudeValue { get; set; }

        [FromQuery(Name = "lat")] 
        public double LatitudeValue { get; set; }

        [FromQuery(Name = "radius")]
        public int Radius { get; set; }

        internal GeoCoordinates Geolocation => new() {Latitude = LatitudeValue, Longitude = LongitudeValue};
    }
}