using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Dal.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Price)
                .IsRequired()
                .HasPrecision(10,2);
                
            builder.Property(d => d.ImageUrl)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(d => d.Category)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Restaturant)
                .WithMany(r => r.Dishes)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.DishOptions)
                .WithOne(o => o.Dish)
                .HasForeignKey(o => o.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            
                
                

        }
    }
}
