using SportsGoods.App.Services;

namespace SportsGoods.App.Helper
{
    public class Orchestrator
    {
        private readonly BrandService _brandService;
        private readonly ProductService _productService;

        public Orchestrator(BrandService brandService, ProductService productService)
        {
            _brandService = brandService;
            _productService = productService;
        }

        public async Task RunAsync(string xmlFilePath)
        {
            await _brandService.ExtractBrandsFromXmlAsync(xmlFilePath);

            await _productService.SeedProductsFromXmlAsync(xmlFilePath);
        }

    }
}
