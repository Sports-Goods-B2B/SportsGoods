using SportsGoods.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoods.Core.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void Add(Product product);
        Task SaveChangesAsync();
    }
}
