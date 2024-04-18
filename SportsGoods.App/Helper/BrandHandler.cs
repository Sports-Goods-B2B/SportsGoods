using SportsGoods.App.Services;
using System.Reflection;

namespace SportsGoods.App.Helper
{
    public class BrandHandler : DataImportHander
    {
        private readonly BrandService _brandService;
        private bool _isDataImported = false;

        public BrandHandler(BrandService brandService)
        {
            _brandService = brandService;
        }

        protected override async Task ImportData()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            var solutionDirectory = Path.Combine(assemblyDirectory, "..", "..", "..", "..");
            var testDataDirectory = Path.Combine(solutionDirectory, "SolutionItems");
            var xmlPath = Path.Combine(testDataDirectory, "products.xml");
            
            await _brandService.ExtractBrandsFromXmlAsync(xmlPath);
            _isDataImported = true;
        }

        protected override bool IsDataImportSuccess() => _isDataImported;

    }
}
