using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Entities
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public static bool operator ==(BaseEntity? first, BaseEntity? second)
        {
            return first != null && second != null && first.Equals(second);
        }
        public static bool operator !=(BaseEntity? first, BaseEntity? second)
        {
            return !(first == second);

        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            if (obj.GetType() != GetType())
                return false;

            if (obj is not BaseEntity baseEntity)
                return false;
            return baseEntity.Id == Id;
        }

        public bool Equals(BaseEntity? other)
        {
            if (other is null)
                return false;
            if (other.GetType() != GetType())
                return false;
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 10;
        }
    }
}
