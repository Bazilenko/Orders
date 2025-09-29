using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;

namespace Dal.Repository.Interfaces
{
    public interface IOrderDishRepository : IGenericRepository<OrderDish>
    {
        Task<IEnumerable<OrderDish>> GetByOrderIdAsync(int orderId);
    }
}
