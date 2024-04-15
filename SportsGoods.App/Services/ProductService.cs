using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using SportsGoods.Data.DAL.EntityConfiguration;
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
        public ProductService(ApplicationDbContext context, IProductRepository productRepository)
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
                var product = await ValidateProduct(element,products);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }

        private async Task<Product?> ValidateProduct(XElement element, List<Product> products)
        {
            var idElement = element.Element("Id");
            var titleElement = element.Element("Title");
            var descriptionElement = element.Element("Description");
            var brandElement = element.Element("Brand");
            var priceElement = element.Element("Price");
            var quantityElement = element.Element("Quantity");
            var productCategoryElement = element.Element("ProductCategory");

            Guid productId;

            if (idElement != null && Guid.TryParse(idElement.Value, out productId) &&
                !string.IsNullOrEmpty(titleElement?.Value) &&
                !string.IsNullOrEmpty(brandElement?.Value) &&
                priceElement != null && double.TryParse(priceElement.Value, out double price) && price >= 0 &&
                quantityElement != null && int.TryParse(quantityElement.Value, out int quantity) && quantity >= 0 &&
                !string.IsNullOrEmpty(productCategoryElement?.Value))
            {

                var existingProductInDb = await _productRepository.GetByIdAsync(productId);

                var existingProductInList = products.FirstOrDefault(p => p.Id == productId);

                if (existingProductInDb == null && existingProductInList == null)
                {
                    return new Product
                    {
                        Id = productId,
                        Title = titleElement.Value,
                        Description = descriptionElement?.Value ?? string.Empty,
                        Brand = new Brand
                        {
                            Id = Guid.NewGuid(),
                            Name = brandElement.Value,
                            History = "test History",
                            PictureUrl = "testUrl"
                        },
                        Price = price,
                        Quantity = quantity,
                        ProductCategory = productCategoryElement.Value
                    };
                }
            }

            return null;
        }
    }
}
