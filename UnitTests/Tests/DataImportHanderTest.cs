using Moq;
using NUnit.Framework;
using SportsGoods.App.Tests.TestHelpers;
using SportsGoods.Core.Interfaces;

namespace SportsGoods.App.Tests.Tests
{
    [TestFixture]
    public class DataImportHanderTest
    {
        [Test]
        public async Task HandleDataFromXML_Success()
        {
            var brandServiceMock = new Mock<IBrandService>();
            var productServiceMock = new Mock<IProductService>();

            var handler1 = new TestHandler1(brandServiceMock.Object);
            var handler2 = new TestHandler2(productServiceMock.Object);

            handler1.SetNextHandler(handler2);

            brandServiceMock.Setup(b => b.ExtractBrandsFromXmlAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
            productServiceMock.Setup(p => p.SeedProductsFromXmlAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            await handler1.HandleDataFromXML();

            brandServiceMock.Verify(b => b.ExtractBrandsFromXmlAsync(It.IsAny<string>()), Times.Once);
            productServiceMock.Verify(p => p.SeedProductsFromXmlAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task HandleDataFromXML_NoNextHandler_Success()
        {
            var brandServiceMock = new Mock<IBrandService>();
            var productServiceMock = new Mock<IProductService>();

            var handler1 = new TestHandler1(brandServiceMock.Object);

            brandServiceMock.Setup(b => b.ExtractBrandsFromXmlAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
            productServiceMock.Setup(p => p.SeedProductsFromXmlAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            await handler1.HandleDataFromXML();

            brandServiceMock.Verify(b => b.ExtractBrandsFromXmlAsync(It.IsAny<string>()), Times.Once);
            productServiceMock.Verify(p => p.SeedProductsFromXmlAsync(It.IsAny<string>()), Times.Never);
        }


    }
}
