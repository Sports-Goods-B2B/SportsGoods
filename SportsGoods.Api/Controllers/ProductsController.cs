using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportsGoods.App.DTOs;
using SportsGoods.App.Queries;
using SportsGoods.Core.Models;

namespace YourApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<PagedResult<ProductDTO>>> GetAllProducts([FromQuery] int pageNumber = 0, [FromQuery] byte pageSize = 10)
        {
            var query = new GetAllProductsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
