﻿using Microsoft.AspNetCore.Identity;

namespace SportsGoods.Core.Models
 
{
    public class Administrator : IdentityUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
