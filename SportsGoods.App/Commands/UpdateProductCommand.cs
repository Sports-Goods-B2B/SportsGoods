using MediatR;
using SportsGoods.Core.Models;

namespace SportsGoods.App.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string BrandName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductCategory { get; set; } = string.Empty;
    }
}
