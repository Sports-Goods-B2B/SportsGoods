using Microsoft.AspNetCore.Identity;

namespace SportsGoods.Core.Models
 
{
    public class Administrator : IdentityUser
    {
        public string Password { get; set; } = string.Empty;

    }
}
