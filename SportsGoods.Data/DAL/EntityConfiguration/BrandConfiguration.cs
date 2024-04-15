using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsGoods.Core.Models;

namespace SportsGoods.Data.DAL.EntityConfiguration
{
    public class BrandConfiguration
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
             .IsRequired();

            builder.Property(p => p.Name)
            .IsRequired();

            builder.Property(p => p.History);

            builder.Property(p => p.Picture);

            builder.ToTable("Brands");
        }
    }
}
