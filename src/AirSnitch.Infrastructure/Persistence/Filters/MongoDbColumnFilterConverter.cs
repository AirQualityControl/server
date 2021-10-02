using System;
using AirSnitch.Infrastructure.Abstract.Persistence;
using AirSnitch.Infrastructure.Abstract.Persistence.Exceptions;
using AirSnitch.Infrastructure.Abstract.Persistence.Filters;
using AirSnitch.Infrastructure.Abstract.Persistence.Query;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AirSnitch.Infrastructure.Persistence.Filters
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
            var value = columnFilter.Column is PrimaryColumn
                ? (object) ConvertToObjectId(columnFilter.Value.ToString())
                : columnFilter.Value.ToString();
            return value;
        }

        private static string ConvertToObjectId(string columnFilterValue)
        {
            var isObjectIdFormatValid = Guid.TryParse(columnFilterValue, out Guid id);
            if (isObjectIdFormatValid)
            {
                return id.ToString();
            }
            throw new InvalidIdFormatException("Specified resource identifier has an invalid format." +
                                               "Please make sure that you pass a correct id.");
        }
    }
}