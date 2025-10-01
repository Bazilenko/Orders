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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Customers")
        {
        }

        public async Task<Customer?> GetByEmailAsync(string email, CancellationToken ct)
        {
            string query = "SELECT * FROM Customers WHERE Email = @email";
            using (SqlCommand cmd = new SqlCommand(query, _dbConnection, (SqlTransaction)_dbTransaction))
            {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync(ct))
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Customer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Email = reader.GetString(reader.GetOrdinal("email")),

                            };
                        }

                    }  
            }
            return null;
        }

        public async Task<Customer?> GetByNameAsync(string name, CancellationToken ct)
        {
            string query = "SELECT * FROM Customers WHERE Name = @name";
            using (SqlCommand cmd = new SqlCommand(query, _dbConnection, (SqlTransaction)_dbTransaction))
            {
                {
                    cmd.Parameters.AddWithValue("@name", name);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync(ct))
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Customer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Email = reader.GetString(reader.GetOrdinal("email")),

                            };
                        }

                    }
                }
            }
            return null;
        }
    }
}
