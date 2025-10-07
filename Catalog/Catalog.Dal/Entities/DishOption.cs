using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Entities
{
    public class DishOption : BaseEntity
    {
        public string Name { get; set; }
        public decimal ModifierPrice { get; set; }
        public int DishId { get; set; }

        public Dish Dish { get; set; } = null!;
    }
}
