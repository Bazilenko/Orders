using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;
using Dal.Repository;
using Dal.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Orders.Dal.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Orders")
        {
        }

        public Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetWithItemsByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
