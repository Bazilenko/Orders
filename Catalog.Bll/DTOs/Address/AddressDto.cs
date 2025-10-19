using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace Catalog.Bll.DTOs.Address
{
    public class AddressDto
    {
        public int RestaurantId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string? PostalCode { get; set; }
    }
}
