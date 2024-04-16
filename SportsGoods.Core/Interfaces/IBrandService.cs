namespace SportsGoods.Core.Interfaces
{
    public interface IBrandService
    {
        Task<Guid?> GetBrandIdByNameAsync(string brandName);
    }
}
