using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Repositories.Interfaces
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        Task<Restaurant?> GetByIdWithFullInfo(int id);
        Task<IEnumerable<Restaurant>> GetByRatingAsync(decimal minRating);
        Task<IEnumerable<Restaurant>> SearchByNameAsync(string name);
        Task<IEnumerable<Restaurant>> GetByCityAsync(string city);
        Task<IEnumerable<Restaurant>> GetActiveAsync();
    }
}
