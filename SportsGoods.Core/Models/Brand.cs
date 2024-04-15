namespace SportsGoods.Core.Models
{
    public class Brand
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string History { get; set; } = string.Empty;
        public Media? Picture { get; set; }
    }
}
