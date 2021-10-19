using System;
using System.Collections.Generic;
using System.Linq;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using AirSnitch.Infrastructure.Persistence.Query;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace AirSnitch.Infrastructure.Persistence.Repositories.Common
{
    internal class MongoDbQueryResultFormatter : IQueryResultFormatter
    {
        public IReadOnlyCollection<IQueryResultEntry> FormatResult(object result, ICollection<string> includedResources)
        {
            var queryResultEntryCollection = new List<MongoDbQueryResultEntry>();

            foreach (var bsonDocument in (List<BsonDocument>)result)
            {
                var json = JObject.Parse(bsonDocument.ToJson());
                var mdbQueryResultEntry = new MongoDbQueryResultEntry()
                {
                    IncludedValues = json.Properties().Where(
                            p => includedResources.Any(r=> r == p.Name)
                        )
                    .ToDictionary(k => k.Name, v => (object) v.Value),
                    
                    ScalarValues = json.Properties().Where(
                             p => includedResources.Any(r=> r != p.Name)
                        )
                    .ToDictionary(k => k.Name, v => (object) v.Value),
                };
                queryResultEntryCollection.Add(mdbQueryResultEntry);
            }
            
            return queryResultEntryCollection;
        }
    }
}