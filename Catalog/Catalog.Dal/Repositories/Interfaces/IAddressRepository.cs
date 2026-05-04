using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Repositories.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<IEnumerable<Address>> GetAddressesByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<Address>> GetAddressesByCityAsync(string city);

    }
}
