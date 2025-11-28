using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
namespace Delivery.Domain.Common
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        [BsonId]
        public string Id { get;  init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; protected set;}

        protected BaseEntity() {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }

        protected void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public static bool operator ==(BaseEntity? first,  BaseEntity? second)
        {
            return first is not null && second is not null && first.Equals(second);
        }

        public static bool operator !=(BaseEntity? first, BaseEntity? second)
        {
            return !(first == second);
        }
        public bool Equals(BaseEntity? other)
        {
            if (other is null)
                return false;
            if (other.GetType() != GetType())
                return false;
            return other.Id == Id;
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) { return false; }
            if (obj.GetType() != GetType()) { return false; }
            if (obj is not BaseEntity baseEntity)
                return false;
            return baseEntity.Id == Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 41;
        }
    }
}
