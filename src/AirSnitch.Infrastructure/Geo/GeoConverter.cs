using System;

namespace AirSnitch.Core.Infrastructure.Geo
{
    /// <summary>
    /// Utility class that provide method for geo converting.
    /// </summary>
    public static class GeoConverter
    {
        // Semi-axes of WGS-84 geoidal reference
        private const double WGS84_a = 6378137.0; // Major semiaxis [m]
        private const double WGS84_b = 6356752.3; // Minor semiaxis [m]
        public static double ConvertDegreesToRadian(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            return 180.0 * radians / Math.PI;
        }
        
        //Earth radius at a given latitude, according to the WGS-84 ellipsoid [m]
        public static double GetEarthRadius(double lat)
        {
            // http://en.wikipedia.org/wiki/Earth_radius
            var An = WGS84_a * WGS84_a * Math.Cos(lat);
            var Bn = WGS84_b * WGS84_b * Math.Sin(lat);
            var Ad = WGS84_a * Math.Cos(lat);
            var Bd = WGS84_b * Math.Sin(lat);
            return Math.Sqrt((An*An + Bn*Bn) / (Ad*Ad + Bd*Bd));
        }
    }
}