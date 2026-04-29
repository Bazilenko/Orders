using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Orders.Dal.Context;
using Orders.Dal.Entities;
using Orders.Dal.Repository;
using Orders.Dal.Repository.Interfaces;
using Orders.Dal.Context.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Orders.Dal.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDapperContext context) : base(context, "Orders")
        {
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            string query = "SELECT Id, CustomerId, RestaurantId, OrderDate, Updated_at AS UpdatedAt, Status, TotalAmount " +
                           "FROM Orders WHERE CustomerId = @customerId";

            return await _context.Connection.QueryAsync<Order>(query, 
                new { customerId }, 
                transaction: _context.Transaction);
        }

        public async Task<decimal> GetIncomeByDate(DateTime fromDate, DateTime toDate)
        {
            var query = "SELECT SUM(TotalAmount) FROM Orders WHERE OrderDate BETWEEN @fromDate AND @toDate AND Status = 'Success'";
            
            var result = await _context.Connection.ExecuteScalarAsync<decimal?>(query, 
                new { fromDate, toDate }, 
                transaction: _context.Transaction);
                
            return result ?? 0m;
        }

        public async Task<Order?> GetWithItemsByIdAsync(int orderId)
        {
            string query = @"
                SELECT 
                    o.Id, o.CustomerId, o.RestaurantId, o.OrderDate, o.Status, o.TotalAmount,
                    d.Id, d.OrderId, d.DishId, d.Quantity, d.PriceAtTimeOfOrder
                FROM Orders o
                LEFT JOIN OrderDishes d ON o.Id = d.OrderId
                WHERE o.Id = @orderId";

            var orderDictionary = new Dictionary<int, Order>();

            var list = await _context.Connection.QueryAsync<Order, OrderDish, Order>(
                query,
                (order, dish) =>
                {
                    if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                    {
                        orderEntry = order;
                        orderEntry.Dishes = new List<OrderDish>();
                        orderDictionary.Add(orderEntry.Id, orderEntry);
                    }

                    if (dish != null)
                    {
                        orderEntry.Dishes.Add(dish);
                    }

                    return orderEntry;
                },
                new { orderId },
                transaction: _context.Transaction,
                splitOn: "Id" 
            );

            return list.FirstOrDefault();
        }
    }
}