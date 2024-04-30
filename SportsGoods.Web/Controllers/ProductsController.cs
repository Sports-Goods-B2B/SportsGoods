using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportsGoods.App.Commands;
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
        //[HttpGet("/products/{productId}")]
        //public async Task<IActionResult> GetProduct(Guid productId)
        //{
        //    var product = await _context.Products.FindAsync(productId);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.ProductId)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }

            return NoContent();
        }
    }
}
