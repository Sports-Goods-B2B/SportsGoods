﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsGoods.Core.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}
