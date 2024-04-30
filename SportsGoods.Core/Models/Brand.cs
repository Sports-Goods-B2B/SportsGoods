namespace SportsGoods.Core.Models
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string History { get; set; } = string.Empty;
        public Media? Picture { get; set; } 
    }
}
