using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories.Interfaces;

namespace Catalog.Dal.Repositories
{
    public class DishOptionRepository : GenericRepository<DishOption>, IDishOptionRepository
    {
        private readonly MyDbContext _dbContext;
        public DishOptionRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
