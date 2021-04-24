using System.Collections.Generic;
using AirSnitch.Core.Domain.Models;
using MongoDB.Bson;

namespace AirSnitch.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// MongoDb aggregation pipeline for lookup monitoring stations by geolocation
    /// </summary>
    internal static class NearestStationsAggregationPipeline
    {
        /// <summary>
        /// Factory method that create a new instance of <see cref="NearestStationsAggregationPipeline"/>
        /// </summary>
        /// <param name="geoLocation"></param>
        /// <param name="numberOfNearestStation"></param>
        /// <returns></returns>
        public static List<BsonDocument> Create(GeoLocation geoLocation, int numberOfNearestStation)
        {
            int numOfPipelineStages = 2;
            
            var geoNearOptions = new BsonDocument
            {
                {
                    "near", new BsonDocument
                    {
                        {"type", "Point"},
                        {"coordinates", new BsonArray {geoLocation.Longitude, geoLocation.Latitude}},
                    }
                },
                {"key", "location"},
                {"distanceField", "dist.calculated"},
            };

            var limitOptions = new BsonDocument()
            {
                {"$limit", numberOfNearestStation}
            };

            var pipeline = new List<BsonDocument>(numOfPipelineStages)
            {
                new BsonDocument {{"$geoNear", geoNearOptions}},
                limitOptions
            };

            return pipeline;
        }
    }
}