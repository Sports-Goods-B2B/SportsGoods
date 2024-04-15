namespace SportsGoods.Core.Models
{
    public class Media
    {
        public Guid Id { get; set; }
        public required byte[] Blob { get; set; }
    }
}
