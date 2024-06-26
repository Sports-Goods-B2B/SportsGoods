﻿namespace SportsGoods.Core.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Brand? Brand { get; set; }
        public Guid? BrandId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductCategory { get; set; } = string.Empty;
    }
}
