using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Dal.Configurations
{
    public class DishOptionConfiguration : IEntityTypeConfiguration<DishOption>
    {
        public void Configure(EntityTypeBuilder<DishOption> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.ModifierPrice)
                .IsRequired()
                .HasPrecision(10,2);

            builder.HasOne(d => d.Dish)
                .WithMany(d => d.DishOptions)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
