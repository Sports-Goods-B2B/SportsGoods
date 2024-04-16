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

        public ProductService(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public async Task SeedProductsFromXmlAsync(string xmlFilePath)
        {
            XDocument doc = XDocument.Load(xmlFilePath);

            var products = ExtractProducts(doc);

            foreach (var product in products)
            {
                await ValidateAndAddProductAsync(product);
            }

            await _context.SaveChangesAsync();
        }

        private List<Product> ExtractProducts(XDocument doc)
        {
            var products = new List<Product>();

            var brands = _context.Brands.ToDictionary(x => x.Name, x => x);

            foreach (var element in doc.Descendants("Product"))
            {
                var productName = element.Element("Title")?.Value;
                var productBrandName = element.Element("Brand")?.Value;

                if (!string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(productBrandName))
                {
                    var product = new Product
                    {
                        Id = Guid.Parse(element.Element("Id")?.Value),
                        Title = productName,
                        Description = element.Element("Description")?.Value,
                        Price = double.Parse(element.Element("Price")?.Value),
                        Quantity = int.Parse(element.Element("Quantity")?.Value),
                        ProductCategory = element.Element("ProductCategory")?.Value,
                    };

                        product.BrandId = brands[productBrandName].Id;
                   
                    products.Add(product);
                }
            }
            return products;
        }



        private async Task ValidateAndAddProductAsync(Product product)
        {
            if (!ValidateProduct(product))
            {
                return;
            }

            if (_context.Products.Any(p => p.Id == product.Id))
            {
                return;
            }

            _context.Products.Add(product);
        }

        private bool ValidateProduct(Product product)
        {
            return !string.IsNullOrEmpty(product.Title) &&
                   !string.IsNullOrEmpty(product.ProductCategory) &&
                   product.Price >= 0 &&
                   product.Quantity >= 0;
        }
    }
}