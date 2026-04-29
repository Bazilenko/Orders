using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Dal.Entities;

namespace Orders.Dal.Repository.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
        Task<Order?> GetWithItemsByIdAsync(int orderId);
        Task<decimal> GetIncomeByDate(DateTime fromDate, DateTime toDate);

    }
}
