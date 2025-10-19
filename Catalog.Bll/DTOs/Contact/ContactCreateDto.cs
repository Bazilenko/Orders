using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Bll.DTOs.Contact
{
    public class ContactCreateDto
    {
        public int RestaurantId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
