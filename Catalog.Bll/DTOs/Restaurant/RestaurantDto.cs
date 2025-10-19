using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Bll.DTOs.Address;
using Catalog.Bll.DTOs.Contact;
using Catalog.Dal.Entities;

namespace Catalog.Bll.DTOs.Restaurant
{
    public class RestaurantDto
    {
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();
        public ICollection<ContactDto> Contacts { get; set; } = new List<ContactDto>();
    }
}
