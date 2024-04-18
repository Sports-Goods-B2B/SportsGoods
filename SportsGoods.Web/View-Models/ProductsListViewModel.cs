namespace SportsGoods.Web.View_Models
{
    public class ProductsListViewModel
    {
        public List<ProductViewModel> Products { get; set; } = null!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
