using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SportsGoods.App.Queries;
using SportsGoods.App.QueryHandlers;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using SportsGoods.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task Handle_ReturnsCorrectPageBasedOnPageSize()
        {
            var pageNumber = 1;
            byte[] pageSizes = { 10, 5, 20 };
            var expectedCounts = new[] { 10, 5, 20 };
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock.Setup(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<byte>()))
                          .ReturnsAsync((int page, byte pageSize) =>
                          {
                              var products = Enumerable.Range(1, 100)
                                                        .Select(i => new Product { Id = Guid.NewGuid(), Title = $"Product {i}", Price = i * 10 })
                                                        .ToList();
                              var pagedProducts = new PagedResult<Product>
                              {
                                  Items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                                  Page = page,
                                  PageSize = pageSize,
                                  TotalCount = products.Count
                              };

                              return pagedProducts;
                          });

            var handler = new GetAllProductsQueryHandler(repositoryMock.Object);
            var queries = pageSizes.Select(pageSize => new GetAllProductsQuery
            { PageNumber = pageNumber, PageSize = pageSize });

            foreach (var query in queries)
            {
                var result = await handler.Handle(query, CancellationToken.None);

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Items.Count
                    , Is.EqualTo(expectedCounts[Array.IndexOf(pageSizes, query.PageSize)]));
            }
        }
        [Test]
        public async Task Handle_ThrowsException_WhenPageNumberOrPageSizeBelowZero()
        {
            var pageNumber = -1; 
            byte[] pageSizes = { 10, 5, 20 };
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock.Setup(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<byte>()))
                          .ReturnsAsync((int page, byte pageSize) =>
                          {
                              var products = Enumerable.Range(1, 100)
                                                        .Select(i => new Product { Id = Guid.NewGuid(), Title = $"Product {i}", Price = i * 10 })
                                                        .ToList();
                              var totalCount = products.Count;
                              return new PagedResult<Product>
                              { 
                                  Items = new List<Product>(),
                                  Page = page,
                                  PageSize = pageSize, 
                                  TotalCount = totalCount 
                              };
                          });

            var handler = new GetAllProductsQueryHandler(repositoryMock.Object);
            var queries = pageSizes.Select(pageSize => new GetAllProductsQuery
            { PageNumber = pageNumber, PageSize = pageSize });

            foreach (var query in queries)
            {
                try
                {
                    await handler.Handle(query, CancellationToken.None);
                    Assert.Fail("Expected exception not thrown");
                }
                catch (ArgumentException ex)
                {
                    Assert.That(ex.Message,Is.EqualTo("Page number and page size must be 0 or greater."));
                }
            }
        }
        [Test]
        public async Task Handle_ReturnsEmptyList_WhenPageNumberOutOfRange()
        {
            var outOfRangePageNumbers = new[] { 100, 200, 50 };
            byte[] pageSizes = { 10, 5, 20 };
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock.Setup(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<byte>()))
                          .ReturnsAsync((int page, byte pageSize) =>
                          {
                              var products = Enumerable.Range(1, 100)
                                                        .Select(i => new Product { Id = Guid.NewGuid(), Title = $"Product {i}", Price = i * 10 })
                                                        .ToList();
                              var totalCount = products.Count;
                              return new PagedResult<Product>
                              {
                                  Items = new List<Product>(),
                                  Page = page,
                                  PageSize = pageSize,
                                  TotalCount = totalCount
                              };
                          });

            var handler = new GetAllProductsQueryHandler(repositoryMock.Object);

            foreach (var pageNumber in outOfRangePageNumbers)
            {
                var queries = pageSizes.Select(pageSize => new GetAllProductsQuery
                { PageNumber = pageNumber, PageSize = pageSize });

                foreach (var query in queries)
                {
                    var result = await handler.Handle(query, CancellationToken.None);

                    Assert.That(result, Is.Not.Null);
                    Assert.That(result.Items, Is.Empty);
                    Assert.That(query.PageSize, Is.EqualTo(result.PageSize));
                }
            }
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
                //TotalPages = 1 
            };
        }
    }
}
