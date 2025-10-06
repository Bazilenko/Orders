using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
