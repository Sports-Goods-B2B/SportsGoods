using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SportsGoods.App.Queries;
using SportsGoods.App.QueryHandlers;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using SportsGoods.Data.Repositories;

namespace SportsGoods.App.Tests.Tests
{
    [TestFixture]
    public class GetAllProductsQueryHandlerTest
    {
        private ApplicationDbContext _context;
        private GetAllProductsQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            SeedTestData();

            var repository = new ProductRepository(_context);
            _handler = new GetAllProductsQueryHandler(repository);
        }

        [Test]
        public async Task Handle_ReturnsPagedResultWithProductDTOs()
        {
            var pageNumber = 1;
            byte pageSize = 10;

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.GetPagedAsync(pageNumber, pageSize))
                          .ReturnsAsync(GetTestProducts());

            var queryHandler = new GetAllProductsQueryHandler(mockRepository.Object);
            var query = new GetAllProductsQuery { PageNumber = pageNumber, PageSize = pageSize };


            var result = await queryHandler.Handle(query, default);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(2));
        }

        [Test]
        [TestCase(10, 10)]
        [TestCase(5, 5)]
        [TestCase(20, 20)]
        public async Task Handle_ReturnsCorrectPageBasedOnPageSize(byte pageSize, int expectedCount)
        {
            var pageNumber = 1;
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock.Setup(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<byte>()))
                          .ReturnsAsync((int page, byte size) =>
                          {
                              var products = Enumerable.Range(1, 100)
                                                        .Select(i => new Product { Id = Guid.NewGuid(), Title = $"Product {i}", Price = i * 10 })
                                                        .ToList();
                              var pagedProducts = new PagedResult<Product>
                              {
                                  Items = products.Skip((page - 1) * size).Take(size).ToList(),
                                  Page = page,
                                  PageSize = size,
                                  TotalCount = products.Count
                              };

                              return pagedProducts;
                          });

            var handler = new GetAllProductsQueryHandler(repositoryMock.Object);
            var query = new GetAllProductsQuery { PageNumber = pageNumber, PageSize = pageSize };

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(expectedCount));
        }
        [Test]
        [TestCase(-1, 10)]
        [TestCase(-1, 5)]
        [TestCase(-1, 20)]
        public async Task Handle_ThrowsException_WhenPageNumberOrPageSizeBelowZero(int pageNumber, byte pageSize)
        {
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock.Setup(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<byte>()))
                          .ReturnsAsync((int page, byte size) =>
                          {
                              var products = Enumerable.Range(1, 100)
                                                        .Select(i => new Product { Id = Guid.NewGuid(), Title = $"Product {i}", Price = i * 10 })
                                                        .ToList();
                              var totalCount = products.Count;
                              return new PagedResult<Product>
                              {
                                  Items = new List<Product>(),
                                  Page = page,
                                  PageSize = size,
                                  TotalCount = totalCount
                              };
                          });

            var handler = new GetAllProductsQueryHandler(repositoryMock.Object);

            try
            {
                await handler.Handle(new GetAllProductsQuery { PageNumber = pageNumber, PageSize = pageSize }, CancellationToken.None);
                Assert.Fail("Expected exception not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Page number and page size must be 0 or greater."));
            }
        }
        [Test]
        [TestCase(100, 10)]
        [TestCase(200, 5)]
        [TestCase(50, 20)]
        public async Task Handle_ReturnsEmptyList_WhenPageNumberOutOfRange(int pageNumber, byte pageSize)
        {
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock.Setup(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<byte>()))
                          .ReturnsAsync((int page, byte size) =>
                          {
                              var products = Enumerable.Range(1, 100)
                                                        .Select(i => new Product { Id = Guid.NewGuid(), Title = $"Product {i}", Price = i * 10 })
                                                        .ToList();
                              var totalCount = products.Count;
                              return new PagedResult<Product>
                              {
                                  Items = new List<Product>(),
                                  Page = page,
                                  PageSize = size,
                                  TotalCount = totalCount
                              };
                          });

            var handler = new GetAllProductsQueryHandler(repositoryMock.Object);

            var query = new GetAllProductsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Empty);
            Assert.That(query.PageSize, Is.EqualTo(result.PageSize));
        }
        private void SeedTestData()
        {
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Title = "Product 1", Price = 10 },
                new Product { Id = Guid.NewGuid(), Title = "Product 2", Price = 20 },
            };

            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
        private PagedResult<Product> GetTestProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Title = "Product 1", Price = 10 },
                new Product { Id = Guid.NewGuid(), Title = "Product 2", Price = 20 },
            };

            return new PagedResult<Product>
            {
                Items = products,
                Page = 1,
                PageSize = 10,
                TotalCount = products.Count
            };
        }
    }
}
