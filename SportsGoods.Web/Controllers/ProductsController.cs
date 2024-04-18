using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsGoods.Data.DAL;
using SportsGoods.Web.View_Models;

namespace SportsGoods.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;

            var products = await _context.Products
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();

            var productViewModels = products.Select(p => new ProductViewModel
            {
                Title = p.Title,
                Description = p.Description,
                Brand = p.Brand,
                Price = p.Price,
                Quantity = p.Quantity,
                ProductCategory = p.ProductCategory

            }).ToList();

            var totalCount = await _context.Products.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var viewModel = new ProductsListViewModel
            {
                Products = productViewModels,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return View(viewModel);
        }
    }
}
