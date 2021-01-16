﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Model.ProductInfo
{
    public class ProductInfoDTO
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public List<Ingredient> Ingredients { get; set; } = default!;
    }
}
