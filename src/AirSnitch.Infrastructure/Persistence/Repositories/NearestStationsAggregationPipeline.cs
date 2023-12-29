using System.Collections.Generic;
using AirSnitch.Domain.Models;
using MongoDB.Bson;

namespace AirSnitch.Infrastructure.Persistence.Repositories;

internal static class NearestStationsAggregationPipeline
{
    public static List<BsonDocument> Create(GeoCoordinates geoLocation, int numberOfNearestStation)
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
            {"key", "geoLocation"},
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
