using AirSnitch.Core.Domain.Models;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AirSnitch.Infrastructure.Persistence.Mongo.Serializers
{
    /// <summary>
    /// Class that serialize a <see cref="GeoLocation"/> object for storing in DB.
    /// </summary>
    internal class GeoLocationSerializer : SerializerBase<GeoLocation>
    {
        public override GeoLocation Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            BsonReader reader = (BsonReader)context.Reader;

            reader.ReadStartDocument();                           
            reader.ReadName();      
            reader.ReadString();     
            reader.ReadName();        
            reader.ReadStartArray();   
                var geoLocation = new GeoLocation()
                {
                    Longitude = reader.ReadDouble(),
                    Latitude = reader.ReadDouble()
                };                                      
            reader.ReadEndArray();                            
            reader.ReadEndDocument();                             

            return geoLocation;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, GeoLocation value)
        {
            context.Writer.WriteStartDocument();
                context.Writer.WriteName("type");
                context.Writer.WriteString("Point");
                context.Writer.WriteName("coordinates");
                context.Writer.WriteStartArray();
                    context.Writer.WriteDouble(value.Longitude);
                    context.Writer.WriteDouble(value.Latitude);
                context.Writer.WriteEndArray();
            context.Writer.WriteEndDocument();
        }
    }
}