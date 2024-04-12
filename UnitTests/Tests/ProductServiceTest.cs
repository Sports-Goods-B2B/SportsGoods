using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SportsGoods.App.Services;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using System.Reflection;

namespace SportsGoods.App.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ApplicationDbContext _context = null!;

        [SetUp]
        public async Task Setup()
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SportsGoodsTest;Trusted_Connection=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
            .Options;

            _context = new ApplicationDbContext(options);

            _context.Products.RemoveRange(_context.Products);
            await _context.SaveChangesAsync();
        }

        [Test]
        public async Task SeedProductsFromXml_ValidXml_AddsProductsToDatabase()
        {
            var solutionDirectory = GetSolutionDirectory();
            var testDataDirectory = Path.Combine(solutionDirectory, "SolutionItems");
            var xmlFilePath = Path.Combine(testDataDirectory, "products.xml");
          
            var repositoryMock = new Mock<IProductRepository>();

            var productService = new ProductService(_context, repositoryMock.Object);

            var initialProductCount = _context.Products.Count();

            await productService.SeedProductsFromXmlAsync(xmlFilePath);

            var finalProductCount = _context.Products.Count();

            Assert.That(finalProductCount, Is.GreaterThan(initialProductCount));
        }

        [Test]
        public async Task SeedProductsFromXml_InvalidXml_DoesNotAddProductsToDatabase()
        {
            var xmlFileName = "invalid_products.xml";
            var testDataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData");
            var xmlFilePath = Path.Combine(testDataDirectory, xmlFileName);

            var productService = new ProductService(_context);

            var initialProductCount = _context.Products.Count();

            await productService.SeedProductsFromXmlAsync(xmlFilePath);

            var finalProductCount = _context.Products.Count();
            Assert.That(initialProductCount, Is.EqualTo(finalProductCount));
        }

        [Test]
        public void SeedProductsFromXml_NonExistentXmlFile_ThrowsFileNotFoundException()
        {
            var nonExistentXmlFilePath = "nonexistent.xml";

            var productService = new ProductService(_context);

            Assert.ThrowsAsync<FileNotFoundException>(async () =>
            await productService.SeedProductsFromXmlAsync(nonExistentXmlFilePath));
        }

        [Test]
        public async Task SeedProductsFromXml_ExistingId_DoesNotAddToDatabase()
        {
            var xmlFileName = "existing_product.xml";
            var testDataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData");
            var xmlFilePath = Path.Combine(testDataDirectory, xmlFileName);

            var existingProducts = new List<Product>
            {
                new Product
                {
                    Id = new Guid("5f550c07-003e-4534-af07-9abbfacdb540"),
                    Title = "Cosmic Cascade Wall Art",
                    Description = "Tranquil Waters Bath Bomb - Indulge in a relaxing bath experience",
                    Brand = "SerenityStyle",
                    Price = 17.65,
                    Quantity = 90,
                    ProductCategory = "Kitchen &amp; Dining"
                }
            };
    
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(existingProducts);
            mockProductRepository.Setup(p => p.Add(It.IsAny<Product>())).Verifiable();

            var productService = new ProductService(_context, mockProductRepository.Object);

            await productService.SeedProductsFromXmlAsync(xmlFilePath);

            mockProductRepository.Verify(p => p.Add(It.IsAny<Product>()), Times.Never);
        }


        private string GetSolutionDirectory()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var solutionDirectory = Path.Combine(assemblyDirectory, "..", "..", "..", "..");
            return Path.GetFullPath(solutionDirectory);
        }
    }
}
