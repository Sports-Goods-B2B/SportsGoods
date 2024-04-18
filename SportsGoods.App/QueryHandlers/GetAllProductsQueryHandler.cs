using MediatR;
using SportsGoods.App.DTOs;
using SportsGoods.App.Extensions;
using SportsGoods.App.Queries;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;

namespace SportsGoods.App.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResult<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 0 || request.PageSize < 0)
            {
                throw new ArgumentException("Page number and page size must be 0 or greater.");
            }

            var pagedProducts = await _productRepository.GetPagedAsync(request.PageNumber, request.PageSize);

            return new PagedResult<ProductDTO>
            {
                Items = pagedProducts.Items.Select(x => x.ConvertToDto()).ToList(),
                Page = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = pagedProducts.TotalCount
            };
        }


    }
}
