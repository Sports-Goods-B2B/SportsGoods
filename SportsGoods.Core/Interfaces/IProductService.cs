namespace SportsGoods.Core.Interfaces
{
    public interface IProductService
    {
        Task SeedProductsFromXmlAsync(string xmlFilePath);
    }
}
