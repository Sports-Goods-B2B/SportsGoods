using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoods.Core.Models
{
    public class Media
    {
        public Guid Id { get; set; }
        public required byte[] Blob { get; set; }
    }
}
