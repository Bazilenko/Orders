using Delivery.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Delivery.Infrastructure.BsonSerializer
{
    public class GeoCoordinateSerializer: SerializerBase<GeoCoordinate>
    {
        public override GeoCoordinate Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            bsonReader.ReadStartDocument();

            bsonReader.ReadName("Latitude");
            var latitude = bsonReader.ReadDouble();

            bsonReader.ReadName("Longitude");
            var longitude = bsonReader.ReadDouble();

            bsonReader.ReadEndDocument();

            return new GeoCoordinate(latitude, longitude);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, GeoCoordinate value)
        {
            var bsonWriter = context.Writer;
            bsonWriter.WriteStartDocument();
            bsonWriter.WriteName("Latitude");
            bsonWriter.WriteDouble(value.Latitude);
            bsonWriter.WriteName("Longitude");
            bsonWriter.WriteDouble(value.Longitude);
            bsonWriter.WriteEndDocument();
        }
    }
}
