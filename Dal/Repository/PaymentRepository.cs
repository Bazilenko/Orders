using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;
using Dal.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Dal.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Payments")
        {
        }

        public async Task<Payment?> GetByOrderIdAsync(int orderId)
        {
            string query = "SELECT * FROM Payments WHERE OrderId = @orderId";
            using (SqlCommand cmd = new SqlCommand(query, _dbConnection, (SqlTransaction)_dbTransaction))
            {
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Payment
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                PaymentMethod = reader.GetString(reader.GetOrdinal("PaymentMethod"))
                            };
                        }

                    }
                }
            }
            return null;
        }

        public async Task<Payment?> GetByPaymentMethodAsync(string paymentMethod)
        {
            string query = "SELECT * FROM Payments WHERE PaymentMethod = @paymentMethod";
            using (SqlCommand cmd = new SqlCommand(query, _dbConnection, (SqlTransaction)_dbTransaction))
            {
                cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Payment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            OrderId = reader.GetInt32(reader.GetOrdinal("OderId")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                            Status = reader.GetString(reader.GetOrdinal("Status")),
                            PaymentMethod = reader.GetString(reader.GetOrdinal("PaymentMethod"))

                        };
                    }

                }
                return null;
            }
        }

    }
}
