using System.Collections.Generic;
using System.Linq;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Infrastructure.Persistence.Query
{
    internal class MongoDbQueryResultEntry : IQueryResultEntry
    {
        public Dictionary<string, object> ScalarValues { get; internal set; }

        public Dictionary<string, object> IncludedValues { get; internal set; }

        public static IReadOnlyCollection<IQueryResultEntry> BuildFrom(List<BsonDocument> bsonDocumentsCollection)
        {
            var queryResultEntryCollection = new List<MongoDbQueryResultEntry>();

            foreach (var bsonDocument in bsonDocumentsCollection)
            {
                var json = JObject.Parse(bsonDocument.ToJson());
                var mdbQueryResultEntry = new MongoDbQueryResultEntry()
                {
                    IncludedValues = json.Properties().Where(p => p.Name == "airQualityIndex" || p.Name == "airPollution")
                        .ToDictionary(k => k.Name, v => (object) v.Value),
                    ScalarValues = json.Properties().Where(p => p.Name != "airQualityIndex")
                        .ToDictionary(k => k.Name, v => (object) v.Value),
                };
                queryResultEntryCollection.Add(mdbQueryResultEntry);
            }
            
            return queryResultEntryCollection;
        }
    }
}