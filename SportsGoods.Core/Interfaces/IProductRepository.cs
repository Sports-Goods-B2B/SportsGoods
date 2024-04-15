using SportsGoods.Core.Models;

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
