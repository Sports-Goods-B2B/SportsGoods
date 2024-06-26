﻿using SportsGoods.Core.Models;

namespace SportsGoods.Web.View_Models
{
    public class ProductViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? BrandName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ProductCategory { get; set; } = string.Empty;
    }
}
