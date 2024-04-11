using MediatR;
using SportsGoods.App.DTOs;
using SportsGoods.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoods.App.Queries
{
    public class GetAllProductsQuery :IRequest<PagedResult<ProductDTO>>
    {
        public int PageNumber { get; set; }
        public byte PageSize { get; set; }
    }
}
