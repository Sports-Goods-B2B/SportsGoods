using SportsGoods.App.DTOs;
using SportsGoods.Core.Models;

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
                BrandId = product.BrandId,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductCategory = product.ProductCategory
            };
        }
    }
}
