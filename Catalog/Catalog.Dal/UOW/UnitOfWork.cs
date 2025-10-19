using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using Catalog.Dal.Repositories.Interfaces;
using Catalog.Dal.UOW.Interfaces;

namespace Catalog.Dal.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext _dbContext { get; }
        public ICategoryRepository Categories { get; set; }
        public IDishRepository Dishes { get; set; }
        public IRestaurantRepository Restaurants { get; set; }
        public IContactRepository Contacts { get; set; }
        public IAddressRepository Addresses { get; set; }

        public UnitOfWork(MyDbContext dbContext, ICategoryRepository categoryRepository, IDishRepository dishRepository, IRestaurantRepository restaurants, IContactRepository contacts, IAddressRepository addresses)
        {
            _dbContext = dbContext;
            Categories = categoryRepository;
            Dishes = dishRepository;
            Restaurants = restaurants;
            Contacts = contacts;
            Addresses = addresses;
        }

        public void Dispose()
        {
            _dbContext.DisposeAsync();
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
