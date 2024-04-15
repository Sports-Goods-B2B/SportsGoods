using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsGoods.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoods.Data.DAL.EntityConfiguration
{
    public class MediaConfiguration
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
             .IsRequired();

            builder.Property(p => p.Blob)
           .IsRequired();

            builder.ToTable("Media");
        }
    }
}
