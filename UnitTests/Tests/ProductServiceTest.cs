using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SportsGoods.App.Services;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using System.Reflection;

namespace UnitTests.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ApplicationDbContext _context;


        [SetUp]
        public void Setup()
        {
            var connectionString = "Server=.;Database=SportsGoods;Trusted_Connection=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
            .Options;

            _context = new ApplicationDbContext(options);

            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();

        }

        [Test]
        public void SeedProductsFromXml_ValidXml_AddsProductsToDatabase()
        {
            var solutionDirectory = GetSolutionDirectory();
            var testDataDirectory = Path.Combine(solutionDirectory, "SolutionItems");
            var xmlFilePath = Path.Combine(testDataDirectory, "products.xml");

            var productService = new ProductService(_context);

            var initialProductCount = _context.Products.Count();

            productService.SeedProductsFromXml(xmlFilePath);

            var finalProductCount = _context.Products.Count();
            Assert.That(finalProductCount, Is.GreaterThan(initialProductCount));
        }

        [Test]
        public void SeedProductsFromXml_InvalidXml_DoesNotAddProductsToDatabase()
        {
            var xmlFileName = "invalid_products.xml";
            var testDataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData");
            var xmlFilePath = Path.Combine(testDataDirectory, xmlFileName);

            var productService = new ProductService(_context);

            var initialProductCount = _context.Products.Count();

            productService.SeedProductsFromXml(xmlFilePath);

            var finalProductCount = _context.Products.Count();
            Assert.That(initialProductCount, Is.EqualTo(finalProductCount));
        }

        [Test]
        public void SeedProductsFromXml_NonExistentXmlFile_ThrowsFileNotFoundException()
        {
            var nonExistentXmlFilePath = "nonexistent.xml";

            var productService = new ProductService(_context);

            Assert.Throws<FileNotFoundException>(() => productService.SeedProductsFromXml(nonExistentXmlFilePath));
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
