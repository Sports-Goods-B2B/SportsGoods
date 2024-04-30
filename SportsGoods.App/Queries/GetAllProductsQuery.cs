using MediatR;
using SportsGoods.App.DTOs;
using SportsGoods.Core.Models;

namespace SportsGoods.App.Queries
{
    public class GetAllProductsQuery : IRequest<PagedResult<ProductDTO>>
    {
        public int PageNumber { get; set; } = 0;
        public byte PageSize { get; set; } = 10;
    }
}
