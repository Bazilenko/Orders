using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Repositories.Interfaces
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        Task<IEnumerable<Dish?>> GetDishesByCategory(int categoryId);
    }
}
