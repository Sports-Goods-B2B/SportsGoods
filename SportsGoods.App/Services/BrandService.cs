using Microsoft.EntityFrameworkCore;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using SportsGoods.Data.DAL;
using System.Xml.Linq;

namespace SportsGoods.App.Services
{
    public class BrandService : IBrandService

    {
        private readonly ApplicationDbContext _context;
        private readonly IBrandRepository _brandRepository;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }
        public BrandService(ApplicationDbContext context, IBrandRepository brandRepository)
        {
            _context = context;
            _brandRepository = brandRepository;
        }

        public async Task ExtractBrandsFromXmlAsync(string xmlFilePath)
        {
            XDocument doc = XDocument.Load(xmlFilePath);

            var brandNames = doc.Descendants("Product")
                .Select(p => p.Element("Brand")?.Value)
                .Where(b => !string.IsNullOrEmpty(b))
                .Distinct()
                .ToList();

            foreach (var brandName in brandNames)
            {
                await CreateBrandIfNotExistingAsync(new Brand { Name = brandName });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CreateBrandIfNotExistingAsync(Brand brand)
        {
            var existingBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == brand.Name);

            if (existingBrand == null)
            {
                var newBrand = new Brand
                {
                    Id = Guid.NewGuid(),
                    Name = brand.Name,
                    History = "History placeholder"
                };

                _context.Brands.Add(newBrand);
            }
        }
    }
}