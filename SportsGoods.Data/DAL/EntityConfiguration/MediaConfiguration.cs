﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsGoods.Core.Models;

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