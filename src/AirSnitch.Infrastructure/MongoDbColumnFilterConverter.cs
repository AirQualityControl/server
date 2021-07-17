using AirSnitch.Infrastructure.Abstract;
using AirSnitch.Infrastructure.Abstract.Persistence;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure
{
    public class MongoDbColumnFilterConverter : IColumnFilterConverter<BsonDocument>
    {
        protected IBsonSerializer<BsonDocument> DocumentSerializer => BsonSerializer.SerializerRegistry.GetSerializer<BsonDocument>();

        protected IBsonSerializerRegistry SerializerRegistry => BsonSerializer.SerializerRegistry;

        public BsonDocument Convert(IColumnFilter columnFilter)
        {
            var builder = Builders<BsonDocument>.Filter;
            var value = GetColumnValue(columnFilter);

            var filterDefinition = builder.Eq(columnFilter.Column.Path, value);
            return filterDefinition.Render(DocumentSerializer, SerializerRegistry);
        }

        private static object GetColumnValue(IColumnFilter columnFilter)
        {
            object value;
            if (columnFilter.Column is PrimaryColumn)
            {
                value = new ObjectId(columnFilter.Value.ToString());
            }
            else
            {
                value = columnFilter.Value.ToString();
            }

            return value;
        }
    }
}