using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories.Interfaces;
using Catalog.Dal.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Dal.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
{
    private readonly MyDbContext _dbContext;

    public RestaurantRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Restaurant>> GetActiveAsync()
{
    return await _dbSet
        .Where(r => r.IsActive)
        .ToListAsync();
}

    public async Task<Restaurant?> GetByIdWithFullInfo(int id)
    {
        return await _dbSet
            .Include(r => r.Addresses)
            .Include(r => r.Contacts)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Restaurant>> GetByRatingAsync(decimal minRating)
    {
        return await _dbSet
            .Where(r => r.Rating >= minRating)
            .ToListAsync();
    }

    public async Task<IEnumerable<Restaurant>> SearchByNameAsync(string name)
    {
        return await _dbSet
            .Where(r => r.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetByCityAsync(string city)
    {
        return await _dbSet
            .Where(r => r.Addresses.Any(a => a.City == city))
            .ToListAsync();
    }
}
}
