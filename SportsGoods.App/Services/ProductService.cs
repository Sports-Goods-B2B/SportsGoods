using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using System.Xml.Linq;

namespace SportsGoods.App.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProductService(ApplicationDbContext context,IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public async Task SeedProductsFromXmlAsync(string xmlFilePath)
        {
            XDocument doc = XDocument.Load(xmlFilePath);

            var products = new List<Product>();
            foreach (var element in doc.Descendants("Product"))
            {
                var idElement = element.Element("Id");
                var titleElement = element.Element("Title");
                var descriptionElement = element.Element("Description");
                var brandElement = element.Element("Brand");
                var priceElement = element.Element("Price");
                var quantityElement = element.Element("Quantity");
                var productCategoryElement = element.Element("ProductCategory");

                Guid productId;

                if (idElement != null && Guid.TryParse(idElement.Value, out productId))
                {
                    if (!string.IsNullOrEmpty(titleElement?.Value) &&
                        !string.IsNullOrEmpty(brandElement?.Value) &&
                        priceElement != null && !string.IsNullOrEmpty(priceElement.Value) 
                        && (double)priceElement >= 0 &&
                        quantityElement != null && !string.IsNullOrEmpty(quantityElement.Value) 
                        && (int)quantityElement >= 0 &&
                        !string.IsNullOrEmpty(productCategoryElement?.Value))
                    {
                        var existingProductInDb = _context.Products.FirstOrDefault(p => p.Id == productId);
                        var existingProductInProductList = products.FirstOrDefault(p => p.Id == productId);
                     
                        if (existingProductInDb == null && existingProductInProductList == null)
                        {
                            var product = new Product
                            {
                                Id = productId,
                                Title = titleElement.Value,
                                Description = descriptionElement.Value,
                                Brand = brandElement.Value,
                                Price = double.Parse(priceElement.Value),
                                Quantity = int.Parse(quantityElement.Value),
                                ProductCategory = productCategoryElement.Value
                            };
                            products.Add(product);
                        }
                    }
                }
            }

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }
    }
}
