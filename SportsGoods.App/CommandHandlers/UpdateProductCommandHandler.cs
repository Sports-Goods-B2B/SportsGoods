using MediatR;
using Microsoft.EntityFrameworkCore;
using SportsGoods.App.Commands;
using SportsGoods.Data.DAL;

namespace SportsGoods.App.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public UpdateProductCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Name == request.BrandName);

            product.Title = request.Title;
            product.Description = request.Description;
            product.BrandId = brand?.Id;
            product.Quantity = request.Quantity;
            product.Price = request.Price;
            product.ProductCategory = request.ProductCategory;

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
