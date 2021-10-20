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
        public IReadOnlyCollection<IQueryResultEntry> FormatResult(object result, ICollection<string> includedResources = default)
        {
            var queryResultEntryCollection = new List<MongoDbQueryResultEntry>();

            foreach (var bsonDocument in (List<BsonDocument>)result)
            {
                var json = JObject.Parse(bsonDocument.ToJson());
                
                var mdbQueryResultEntry = new MongoDbQueryResultEntry()
                {
                    IncludedValues = GetIncludes(json, includedResources),
                    ScalarValues = GetScalarValues(json, includedResources)
                };
                queryResultEntryCollection.Add(mdbQueryResultEntry);
            }
            
            return queryResultEntryCollection;
        }

        private Dictionary<string, object> GetIncludes(JObject jsonDocument, ICollection<string> includedResources)
        {
            if (includedResources == null)
            {
                return new Dictionary<string, object>();
            }

            return jsonDocument.Properties().Where(
                    p => includedResources.Any(r => r == p.Name)
            ).ToDictionary(k => k.Name, v => (object) v.Value);
        }
        
        private Dictionary<string, object> GetScalarValues(JObject jsonDocument, ICollection<string> includedResources)
        { 
            if (includedResources == null)
            {
                return jsonDocument
                    .Properties()
                    .ToDictionary(
                        k => k.Name,
                        v => (object) v.Value);
            }

            return jsonDocument
                .Properties()
                .Where(p => !includedResources.Contains(p.Name))
                .ToDictionary(
                k => k.Name,
                v => (object) v.Value);
        }

    }
}