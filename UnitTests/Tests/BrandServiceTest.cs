using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SportsGoods.App.Services;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using System.Reflection;

namespace SportsGoods.App.Tests.Tests
{
    [TestFixture]
    public class BrandServiceTest
    {
        private static ApplicationDbContext _testContext = null!;

        [OneTimeSetUp]
        public static async Task OneTimeSetUp()
        {
            var testConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SportsGoodsTest;Trusted_Connection=True;TrustServerCertificate=True;";

            var testDbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(testConnectionString)
                .Options;

             _testContext = new ApplicationDbContext(testDbContextOptions);

            await _testContext.Database.MigrateAsync();
        }

        [SetUp]
        public async Task Setup()
        {
            _testContext.Products.RemoveRange(_testContext.Products);
            _testContext.Brands.RemoveRange(_testContext.Brands);

            await _testContext.SaveChangesAsync();
        }

        [Test]
        public async Task ExtractBrandsFromXmlAsync_AddsNewBrandsToDatabase()
        {
            var solutionDirectory = GetSolutionDirectory();
            var testDataDirectory = Path.Combine(solutionDirectory, "SolutionItems");
            var xmlFilePath = Path.Combine(testDataDirectory, "products.xml");

            var brandService = new BrandService(_testContext);

            await brandService.ExtractBrandsFromXmlAsync(xmlFilePath);

            var brandCount = await _testContext.Brands.CountAsync();
            Assert.That(brandCount, Is.GreaterThan(0));
        }

        [Test]
        public async Task CreateBrandIfNotExistingAsync_AddsNewBrandToDatabase()
        {
            var brand = new Brand 
            { 
                Id = Guid.NewGuid(),
                Name = "New Brand for testing purposes",
                History = "historyPlaceholder"
            };

            await _testContext.Brands.AddAsync(brand);
            await _testContext.SaveChangesAsync();

            var createdBrand = await _testContext.Brands.FirstOrDefaultAsync(b => b.Name == brand.Name);

            Assert.That(createdBrand,Is.Not.Null);
        }
        [Test]
        public async Task ExtractBrandsFromXmlAsync_AddsUniqueBrandsToDatabase()
        {
            var xmlFileName = "duplicated_brand_names.xml";
            var testDataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData");
            var xmlFilePath = Path.Combine(testDataDirectory, xmlFileName);

            var brandService = new BrandService(_testContext);

            await brandService.ExtractBrandsFromXmlAsync(xmlFilePath);

            var brandCount = await _testContext.Brands.CountAsync();
            var distinctBrandCount = await _testContext.Brands.Select(b => b.Name).Distinct().CountAsync();

            Assert.That(brandCount, Is.EqualTo(distinctBrandCount));
        }

        [Test]
        public async Task ExtractBrandsFromXmlAsync_DoesNotAddExistingBrandsToDatabase()
        {
            var xmlFileName = "existing_product.xml";
            var testDataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData");
            var xmlFilePath = Path.Combine(testDataDirectory, xmlFileName);

            var existingBrands = new List<Brand>
            {
                new Brand { Id = Guid.NewGuid(), Name = "MystiqueMarque", History = "historyPlaceholder" },
                new Brand { Id = Guid.NewGuid(), Name = "LuxeLagoon", History = "historyPlaceholder" },
            };

            var mockBrandRepository = new Mock<IBrandRepository>();
            mockBrandRepository.Setup(b => b.GetAllAsync()).ReturnsAsync(existingBrands);

            var brandService = new BrandService(_testContext, mockBrandRepository.Object);

            await brandService.ExtractBrandsFromXmlAsync(xmlFilePath);

            mockBrandRepository.Verify(b => b.Add(It.IsAny<Brand>()), Times.Never);
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
