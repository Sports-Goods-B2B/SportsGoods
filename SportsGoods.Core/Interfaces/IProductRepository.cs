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
        Task<Product> GetByIdAsync(Guid id);
        Task<List<Product>> GetAllAsync();
        Task<PagedResult<Product>> GetPagedAsync(int pageNumber, byte pageSize);
        void Add(Product product);
        Task SaveChangesAsync();
    }
}
