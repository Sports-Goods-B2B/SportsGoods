namespace SportsGoods.Core.Models
{
    public class PagedResult<T>
    {
        public required List<T> Items { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
