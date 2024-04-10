using Microsoft.EntityFrameworkCore;
using SportsGoods.Core.Models;

namespace SportsGoods.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }
    }
}
