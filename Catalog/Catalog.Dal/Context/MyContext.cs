using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Configurations;
using Catalog.Dal.Entities;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Dal.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Dish> Dishes { get; set; } = null!;
        public DbSet<DishOption> DishesOptions { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = CatalogDB; Trusted_Connection = True; ");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new RestaurantConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new DishConfiguration());
            builder.ApplyConfiguration(new DishOptionConfiguration());
            builder.ApplyConfiguration(new ContactConfiguration());
        }
    }
}
