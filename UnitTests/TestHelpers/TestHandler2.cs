using SportsGoods.App.Helper;
using SportsGoods.App.Services;
using SportsGoods.Core.Interfaces;
using System.Reflection;

namespace SportsGoods.App.Tests.TestHelpers
{
    public class TestHandler2 : DataImportHander
    {
        private readonly IProductService _productService;
        private bool _isDataImported = false;

        public TestHandler2(IProductService productService)
        {
            _productService = productService;
        }

        protected override async Task ImportData()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            var solutionDirectory = Path.Combine(assemblyDirectory, "..", "..", "..", "..");
            var testDataDirectory = Path.Combine(solutionDirectory, "SolutionItems");
            var xmlPath = Path.Combine(testDataDirectory, "products.xml");

            await _productService.SeedProductsFromXmlAsync(xmlPath);
            _isDataImported = true;
        }

        protected override bool IsDataImportSuccess() => _isDataImported;
    }
}
