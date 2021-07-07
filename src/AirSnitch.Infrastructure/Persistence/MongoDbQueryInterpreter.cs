using AirSnitch.Infrastructure.Abstract.Persistence;

namespace AirSnitch.Infrastructure.Persistence
{
    public class MongoDbQueryInterpreter : IQueryInterpreter<MongoDbQuery>
    {
        public MongoDbQuery InterpretQuery(QueryScheme queryScheme)
        {
            var mongoQuery = new MongoDbQuery(collectionName: queryScheme.EntityName);
            foreach (var column in queryScheme.Columns)
            {
                mongoQuery.AddColumn(column);
            }
            return mongoQuery;
        }
    }
}