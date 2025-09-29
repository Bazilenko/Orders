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
    public class OrderDishRepository : GenericRepository<OrderDish>, IOrderDishRepository
    {
        public OrderDishRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "OrderDishes")
        {
        }

        public Task<IEnumerable<OrderDish>> GetByOrderIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
