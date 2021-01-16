using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Model.ProductInfo
{
    public sealed class CreateProductInfoRequest
    {
        public string Name { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public List<Ingredient> Ingredients { get; set; } = default!;
    }
}
