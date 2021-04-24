using System;

namespace AirSnitch.Core.Infrastructure.Geo.Exceptions
{
    public class InvalidGeoLocationCoordinate : Exception
    {
        public InvalidGeoLocationCoordinate()
        {
        }

        public InvalidGeoLocationCoordinate(string message) : base(message)
        {
        }

        public InvalidGeoLocationCoordinate(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}