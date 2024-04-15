using Microsoft.EntityFrameworkCore;
using SportsGoods.Core.Models;

namespace SportsGoods.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Administrator> Administrators { get; }
        DbSet<Product> Products { get; }
        public DbSet<Brand> Brands { get; }
        public DbSet<Media> Media { get; }
    }
}
