using Dapper; 
using Orders.Dal.Entities;
using Orders.Dal.Repository.Interfaces;
using Orders.Dal.Context.Interfaces;
namespace Orders.Dal.Repository{
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(IDapperContext context) : base(context, "Customers")
    {
    }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken ct)
    {
        string query = "SELECT * FROM Customers WHERE Email = @email";
        
        return await _context.Connection.QueryFirstOrDefaultAsync<Customer>(
            new CommandDefinition(query, new { email }, transaction: _context.Transaction, cancellationToken: ct)
        );
    }

    public async Task<Customer?> GetByNameAsync(string name, CancellationToken ct)
    {
        string query = "SELECT * FROM Customers WHERE Name = @name";
        
        return await _context.Connection.QueryFirstOrDefaultAsync<Customer>(
            new CommandDefinition(query, new { name }, transaction: _context.Transaction, cancellationToken: ct)
        );
    }
}
}