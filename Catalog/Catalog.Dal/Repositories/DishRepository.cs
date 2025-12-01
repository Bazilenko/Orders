using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories.Interfaces;

namespace Catalog.Dal.Repositories
{
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        private MyDbContext _dbContext;
        public DishRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Dish?>> GetDishesByCategory(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            if (category == null) 
                return Enumerable.Empty<Dish>();

            await _dbContext.Entry(category)
                .Collection(c => c.Dishes)
                .LoadAsync();

            return category.Dishes;
        }
        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            var query = _dbContext.Dishes.Include(d => d.Category);
            var dishes = await query.ToListAsync();

            foreach (var dish in dishes)
            {
                await _dbContext.Entry(dish)
                    .Reference(d => d.Category)
                    .LoadAsync();
            }

            return dishes;
        }

        public async Task<Dish> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var dish = await _dbContext.Dishes.FindAsync(id);
            if (dish != null)
            {
                await _dbContext.Entry(dish)
               .Reference(d => d.Category)
                .LoadAsync();
            }

            return dish;

        }
    }
}
