using AirSnitch.Infrastructure.Persistence.StorageModels;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AirSnitch.Infrastructure.Persistence.Serializers
{
    /// <summary>
    /// Class that serialize a <see cref="GeoLocation"/> object for storing in DB.
    /// </summary>
    internal class GeoLocationSerializer : SerializerBase<GeoLocationStorageModel>
    {
        public override GeoLocationStorageModel Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            BsonReader reader = (BsonReader)context.Reader;

            reader.ReadStartDocument();                           
            reader.ReadName();      
            reader.ReadString();     
            reader.ReadName();        
            reader.ReadStartArray();   
            var geoLocation = new GeoLocationStorageModel()
            {
                Longitude = reader.ReadDouble(),
                Latitude = reader.ReadDouble()
            };                                      
            reader.ReadEndArray();                            
            reader.ReadEndDocument();                             

            return geoLocation;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, GeoLocationStorageModel value)
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