using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Dal.Entities;
using Dal.Repository;
using Dal.Repository.Interfaces;
using Dommel;
using Microsoft.Data.SqlClient;

namespace Orders.Dal.Repository
{
   
    public class OrderDishRepository : GenericRepository<OrderDish>, IOrderDishRepository
    {
        public OrderDishRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "OrderDishes")
        {
        }

        public async Task<IEnumerable<OrderDish?>> GetByOrderIdAsync(int orderId)
        {
            var dishes = await _dbConnection.SelectAsync<OrderDish>(od => od.OrderId == orderId, transaction: _dbTransaction);
            return dishes;
            
        }
    }
}
