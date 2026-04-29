using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Dal.Entities;

namespace Orders.Dal.Repository.Interfaces
{
    public interface IOrderDishRepository : IGenericRepository<OrderDish>
    {
        Task<IEnumerable<OrderDish>> GetByOrderIdAsync(int orderId);
    }
}
