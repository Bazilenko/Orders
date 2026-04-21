using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Bll.DTOs.Address;

namespace Catalog.Bll.DTOs.Restaurant
{
    public class RestaurantCreateDto
    {
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<AddressCreateDto> Addresses { get; set; }
    }
}
