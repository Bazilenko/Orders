using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Bll.DTOs.OrderDish
{
    public class OrderDishDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtTimeOfOrder { get; set; }
    }
}
