using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Value_Objects;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Delivery.Infrastructure.BsonSerializer
{
    public class MoneySerializer : SerializerBase<Money>
    {
        public override Money Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            bsonReader.ReadStartDocument();

            bsonReader.ReadName("Amount");
            var amount = bsonReader.ReadDecimal128();

            bsonReader.ReadName("Currency");
            var currency = bsonReader.ReadString();

            bsonReader.ReadEndDocument();

            return new Money(Decimal128.ToDecimal(amount), currency);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Money value)
        {
            var bsonWriter = context.Writer;
            bsonWriter.WriteStartDocument();
            bsonWriter.WriteName("Amount");
            bsonWriter.WriteDecimal128(value.Amount);
            bsonWriter.WriteName("Currency");
            bsonWriter.WriteString(value.Currency);
            bsonWriter.WriteEndDocument();
        }
    }
    }
