using System.Collections.Generic;
using System.Linq;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Infrastructure.Persistence
{
    internal class MongoDbQueryResultEntry : IQueryResultEntry
    {
        public Dictionary<string, object> ScalarValues { get; internal set; }

        public Dictionary<string, object> IncludedValues { get; internal set; }

        public static IReadOnlyCollection<IQueryResultEntry> BuildFrom(List<BsonDocument> bsonDocumentsCollection)
        {
            var queryResultEntryCollection = new List<MongoDbQueryResultEntry>();
            
            var res2 = bsonDocumentsCollection.Select(r => r.ToJson()).ToList();

            foreach (var res in res2)
            {
                var jsobject = JObject.Parse(res);
                var a = jsobject.ToObject<Dictionary<string, object>>();

                var scalarValues =
                    a.Where(
                            a => a.Value != null &&
                                 a.Value.GetType() != typeof(JObject) &&
                                 a.Value.GetType() != typeof(JArray))
                        .ToDictionary(k => k.Key, v => v.Value);

                var includedResources = new Dictionary<string, object>();

                var jarrayValues =
                    a.Where(a => a.Value != null && a.Value.GetType() == typeof(JArray));

                foreach (var jaraayValue in jarrayValues)
                {
                    includedResources.Add(jaraayValue.Key, jaraayValue.Value);
                }

                var embededDoc =
                    a.SingleOrDefault(
                        a => a.Value != null && a.Value.GetType() == typeof(JObject));

                if (embededDoc.Key != null)
                {
                    includedResources.Add(embededDoc.Key, embededDoc.Value);
                }

                //SWAP here
                if (!scalarValues.Any())
                {
                    if (includedResources.Any())
                    {
                        queryResultEntryCollection.Add(new MongoDbQueryResultEntry()
                        {
                            ScalarValues = includedResources, 
                            IncludedValues = scalarValues
                        });
                        continue;
                    }
                }
                
                queryResultEntryCollection.Add(new MongoDbQueryResultEntry()
                {
                    ScalarValues = scalarValues, 
                    IncludedValues = includedResources
                });
            }

            return queryResultEntryCollection;
        }
    }
}