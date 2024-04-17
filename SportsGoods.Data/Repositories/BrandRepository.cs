using Microsoft.EntityFrameworkCore;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;

namespace SportsGoods.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Brand?> GetByIdAsync(Guid id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public void Add(Brand brand)
        {
            _context.Brands.Add(brand);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
