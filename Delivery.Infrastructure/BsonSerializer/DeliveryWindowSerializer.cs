using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Delivery.Infrastructure.BsonSerializer
{
    public class DeliveryWindowSerializer : SerializerBase<DeliveryWindow>
    {
        public override DeliveryWindow Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            bsonReader.ReadStartDocument();

            bsonReader.ReadName("StartTime");
            var startTime = bsonReader.ReadDateTime();

            bsonReader.ReadName("EndTime");
            var endTime = bsonReader.ReadDateTime();

            bsonReader.ReadEndDocument();

            return new DeliveryWindow(
                DateTime.SpecifyKind(BsonUtils.ToDateTimeFromMillisecondsSinceEpoch(startTime), DateTimeKind.Utc),
                DateTime.SpecifyKind(BsonUtils.ToDateTimeFromMillisecondsSinceEpoch(endTime), DateTimeKind.Utc)
            );
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DeliveryWindow value)
        {
            var bsonWriter = context.Writer;
            bsonWriter.WriteStartDocument();
            bsonWriter.WriteName("Start");
            bsonWriter.WriteDateTime(BsonUtils.ToMillisecondsSinceEpoch(value.Start));
            bsonWriter.WriteName("End");
            bsonWriter.WriteDateTime(BsonUtils.ToMillisecondsSinceEpoch(value.End));
            bsonWriter.WriteEndDocument();
        }
    }
}
