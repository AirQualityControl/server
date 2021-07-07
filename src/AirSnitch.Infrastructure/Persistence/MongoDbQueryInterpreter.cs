using AirSnitch.Infrastructure.Abstract.Persistence;
using MongoDB.Bson;

namespace AirSnitch.Infrastructure.Persistence
{
    public class MongoDbQueryInterpreter : IQueryInterpreter<BsonDocument>
    {
        public BsonDocument InterpretQuery(FetchQuery query)
        {
            return null;
        }
    }
}