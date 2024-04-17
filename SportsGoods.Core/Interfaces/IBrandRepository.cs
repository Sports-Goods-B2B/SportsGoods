using SportsGoods.Core.Models;

namespace SportsGoods.Core.Interfaces
{
    public interface IBrandRepository
    {
        Task<Brand?> GetByIdAsync(Guid id);
        Task<List<Brand>> GetAllAsync();
        void Add(Brand brand);
        Task SaveChangesAsync();
    }
}
