using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using AirSnitch.Core.Domain.Models;



namespace AirSnitch.Core.Infrastructure.Geo
{
    public class BoundingBox
    {
        public GeoLocation MinPoint { get; internal set; }
        public GeoLocation MaxPoint { get; internal set; }

        /// <summary>
        /// Factory method that create a new instance of geolocation bounding box with a special radius size
        /// </summary>
        /// <returns></returns>
        public static BoundingBox Create(GeoLocation point, double halfSideInKm)
        {
            Contract.Requires(point != null);

            // Bounding box surrounding the point at given coordinates,
            // assuming local approximation of Earth surface as a sphere
            // of radius given by WGS84
            var lat = GeoConverter.ConvertDegreesToRadian(point.Latitude);
            var lon = GeoConverter.ConvertDegreesToRadian(point.Longitude);
            var halfSide = 1000 * halfSideInKm;

            // Radius of Earth at given latitude
            var radius = GeoConverter.GetEarthRadius(lat);
            // Radius of the parallel at given latitude
            var pradius = radius * Math.Cos(lat);

            var latMin = lat - halfSide / radius;
            var latMax = lat + halfSide / radius;
            var lonMin = lon - halfSide / pradius;
            var lonMax = lon + halfSide / pradius;

            return new BoundingBox { 
                MinPoint = new GeoLocation() 
                { 
                    Latitude =  GeoConverter.ConvertRadiansToDegrees(latMin), 
                    Longitude = GeoConverter.ConvertRadiansToDegrees(lonMin) 
                },
                MaxPoint = new GeoLocation()
                {
                    Latitude = GeoConverter.ConvertRadiansToDegrees(latMax), 
                    Longitude = GeoConverter.ConvertRadiansToDegrees(lonMax)
                }
            };
        }

        public GeoLocation GetFirstPointFromEnumerationInsideBoundingBox(IEnumerable<GeoLocation> geoLocationPointsEnumeration)
        {
            return geoLocationPointsEnumeration
                .Where(x => x.Latitude >= MinPoint.Latitude && x.Latitude <= MaxPoint.Latitude)
                .Where(x => x.Longitude >= MinPoint.Longitude && x.Longitude <= MaxPoint.Longitude)
                .FirstOrDefault();
        }

        public bool IsPointInBoundingBox(GeoLocation point)
        {
            throw new NotImplementedException();
        }
    }        
}