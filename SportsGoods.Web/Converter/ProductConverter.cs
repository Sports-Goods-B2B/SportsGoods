using SportsGoods.Core.Models;
using SportsGoods.Web.View_Models;

namespace SportsGoods.Web.Converter
{
    public static class ProductConverter
    {
        public static ProductViewModel ConvertToViewModel(this Product product)
        {
            return new ProductViewModel
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
