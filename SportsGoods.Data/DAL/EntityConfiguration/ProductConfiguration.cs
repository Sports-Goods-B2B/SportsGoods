using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsGoods.Core.Models;

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

            builder.HasOne(p => p.Brand)
             .WithMany() 
             .HasForeignKey(p => p.BrandId) 
             .IsRequired(false);

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
