using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orders.Bll.DTOs.OrderDish;

namespace Orders.Bll.DTOs.Order
{
    public class OrderReceiptDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime OrderDate { get; set; } 
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDishDto?> Dishes { get; set; }
    }
}
