using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories.Interfaces;
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

        public async Task<Restaurant?> GetByIdWithFullInfo(int id)
        {
            var restaurant = await _dbContext.Restaurants
            .Include(r => r.Addresses)
            .Include(r => r.Contacts)
            .FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }
    }
}
