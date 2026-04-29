using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Dal.Entities;
using Orders.Dal.Repository;
using Orders.Dal.Repository.Interfaces;
using Orders.Dal.Context.Interfaces; 
using Dommel;

namespace Orders.Dal.Repository
{
    public class OrderDishRepository : GenericRepository<OrderDish>, IOrderDishRepository
    {
        public OrderDishRepository(IDapperContext context) : base(context, "OrderDishes")
        {
        }

        public async Task<IEnumerable<OrderDish?>> GetByOrderIdAsync(int orderId)
        {
            var dishes = await _context.Connection.SelectAsync<OrderDish>(
                od => od.OrderId == orderId, 
                transaction: _context.Transaction
            );

            return dishes;
        }
    }
}