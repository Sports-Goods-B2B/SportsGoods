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
            var products = await _productRepository.GetAllAsync();
            var productDtos = products.Select(p => p.ConvertToDto()).ToList();

            return new PagedResult<ProductDTO>
            {
                Items = productDtos,
                Page = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
