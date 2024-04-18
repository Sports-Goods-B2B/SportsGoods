using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsGoods.Core.Models;

namespace SportsGoods.Data.DAL.EntityConfiguration
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
             .IsRequired();

            builder.Property(p => p.Blob)
             .IsRequired()
             .HasColumnType("varbinary(MAX)");

            builder.ToTable("Media");
        }
    }
}
