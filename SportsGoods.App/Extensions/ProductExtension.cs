using SportsGoods.App.DTOs;
using SportsGoods.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoods.App.Extensions
{
    public static class ProductExtension
    {
        public static ProductDTO ConvertToDto(this Product product)
        {
            return new ProductDTO
            {
                Title = product.Title,
                Description = product.Description,
                Brand = product.Brand,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductCategory = product.ProductCategory
            };
        }
    }
}
