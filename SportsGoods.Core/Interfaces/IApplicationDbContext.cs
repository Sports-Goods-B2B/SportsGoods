using Microsoft.EntityFrameworkCore;
using SportsGoods.Core.Models;

namespace SportsGoods.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Administrator> Administrators{ get; }
        DbSet<Product> Products { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Media> Media { get; }
    }
}
