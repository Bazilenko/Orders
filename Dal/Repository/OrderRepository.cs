using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;
using Dal.Repository;
using Dal.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Orders.Dal.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Orders")
        {
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            string query = "SELECT * FROM Orders WHERE CustomerId = @customerId";

            using (SqlCommand cmd = new SqlCommand(query, _dbConnection, (SqlTransaction)_dbTransaction))
            {
                cmd.Parameters.AddWithValue("@customerId", customerId);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    var orders = new List<Order>();
                    while (await reader.ReadAsync())
                    {
                        var order = new Order
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RestaurantId")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("Updated_at")),
                            Status = reader.GetString(reader.GetOrdinal("Status")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"))
                        };

                        orders.Add(order);
                    }
                    return orders;
                }

            }
        }

        public async Task<decimal> GetIncomeByDate(DateTime fromDate, DateTime toDate)
        {
            var query = "SELECT SUM(TotalAmount) FROM Orders WHERE OrderDate BETWEEN @fromDate AND @toDate AND Status = 'Success'";
            var result = await _dbConnection.ExecuteScalarAsync <decimal ?> (query, new { fromDate, toDate }, _dbTransaction);
            return result ?? 0m;

        }

        /*
        public async Task<Order?> GetWithItemsByIdAsync(int orderId)
        {
            string query = "SELECT * FROM OrderDishes WHERE OrderId = @orderId";

            var order = await GetAsync(orderId);
            if (order == null)
                return null;
            using (SqlCommand cmd = new SqlCommand(query, _dbConnection, (SqlTransaction)_dbTransaction))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        order.Dishes.Add(new OrderDish
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                            DishId = reader.GetInt32(reader.GetOrdinal("DishId")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            PriceAtTimeOfOrder = reader.GetInt32(reader.GetOrdinal("PriceAtTimeOfOrder"))
                        });
                    }
                }
            }
            return null;
        }
        */
    }

}
