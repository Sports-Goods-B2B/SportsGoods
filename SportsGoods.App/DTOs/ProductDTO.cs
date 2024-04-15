namespace SportsGoods.App.DTOs
{
    public class ProductDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductCategory { get; set; } = string.Empty;
    }
}
