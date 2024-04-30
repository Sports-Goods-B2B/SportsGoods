using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsGoods.App.Queries;
using SportsGoods.Data.DAL;
using SportsGoods.Web.Converter;
using SportsGoods.Web.View_Models;

namespace SportsGoods.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        public ProductsController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int pageNumber = 1, byte pageSize = 10)
        {
            var query = new GetAllProductsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);

            var productViewModels = new List<ProductViewModel>();

            foreach (var dto in result.Items)
            {
                var productViewModel = ProductConverter.ConvertToViewModel(dto);
                productViewModels.Add(productViewModel);
            }

            var viewModel = new ProductsListViewModel
            {
                Products = productViewModels,
                PageNumber = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize)
            };

            return View(viewModel);
        }
    }
}
