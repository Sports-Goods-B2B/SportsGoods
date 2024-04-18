using SportsGoods.App.Services;
using System.Reflection;

namespace SportsGoods.App.Helper
{
    public class ProductHandler : DataImportHander
    {
        private readonly ProductService _productService;
        private bool _isDataImported = false;

        public ProductHandler(ProductService productService)
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
