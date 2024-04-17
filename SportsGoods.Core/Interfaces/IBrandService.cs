namespace SportsGoods.Core.Interfaces
{
    public interface IBrandService
    {
        Task ExtractBrandsFromXmlAsync(string xmlFilePath);
    }
}
