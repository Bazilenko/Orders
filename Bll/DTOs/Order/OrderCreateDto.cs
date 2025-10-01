using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Bll.DTOs.Order
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
