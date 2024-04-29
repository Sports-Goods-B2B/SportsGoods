using Microsoft.EntityFrameworkCore;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;

namespace SportsGoods.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<Product>> GetPagedAsync(int pageNumber, byte pageSize)
        {
            int pageSizeInt = pageSize;

            var products = await _context.Products
              .Skip(pageNumber * pageSize)
              .Take(pageSizeInt)
              .ToListAsync();

            var brandIds = products.Select(x => x.BrandId).ToList();
            var brands = await _context.Brands.Where(x => brandIds.Contains(x.Id)).ToListAsync();
            products.ForEach(product => product.Brand = brands.FirstOrDefault(b => b.Id == product.BrandId));

            var totalCount = await _context.Products.CountAsync(); 

            return new PagedResult<Product>
            {
                Items = products,
                Page = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
