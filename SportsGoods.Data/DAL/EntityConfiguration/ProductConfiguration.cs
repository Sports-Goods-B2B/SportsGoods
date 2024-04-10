using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsGoods.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoods.Data.DAL.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
             .IsRequired();

            builder.Property(p => p.Title)
            .IsRequired();

            builder.Property(p => p.Brand)
            .IsRequired();

            builder.Property(p => p.Quantity)
            .IsRequired();
            builder.Property(p => p.Quantity)
                .HasAnnotation("CheckNegativeNumberConstraint", "CHECK (Quantity >= 0)");

            builder.Property(p => p.Price)
            .IsRequired();
            builder.Property(p => p.Price)
                .HasAnnotation("CheckNegativeNumberConstraint", "CHECK (Price >= 0)");

            builder.Property(p => p.ProductCategory)
            .IsRequired();

            builder.ToTable("Products");
        }
    }
}
