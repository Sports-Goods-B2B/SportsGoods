using MediatR;
using SportsGoods.App.DTOs;
using SportsGoods.App.Extensions;
using SportsGoods.App.Queries;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var productDtos = new List<ProductDTO>();

            foreach (var product in pagedProducts.Items)
            {
                productDtos.Add(product.ConvertToDto());
            }

            var pagedResult = new PagedResult<ProductDTO>
            {
                Items = productDtos,
                Page = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = pagedProducts.TotalCount
            };

            return pagedResult;
        }
    }
}
