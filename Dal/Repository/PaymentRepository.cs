using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Orders.Dal.Context;
using Orders.Dal.Entities;
using Orders.Dal.Repository;
using Orders.Dal.Repository.Interfaces;
using Orders.Dal.Context.Interfaces;
using Dapper;

namespace Orders.Dal.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDapperContext context) : base(context, "Payments")
        {
        }

        public async Task<Payment?> GetByOrderIdAsync(int orderId)
        {
            string query = "SELECT * FROM Payments WHERE OrderId = @orderId";

            return await _context.Connection.QueryFirstOrDefaultAsync<Payment>(
                query, 
                new { orderId }, 
                transaction: _context.Transaction
            );
        }

        public async Task<Payment?> GetByPaymentMethodAsync(string paymentMethod)
        {
            string query = "SELECT * FROM Payments WHERE PaymentMethod = @paymentMethod";

            return await _context.Connection.QueryFirstOrDefaultAsync<Payment>(
                query, 
                new { paymentMethod }, 
                transaction: _context.Transaction
            );
        }
    }
}