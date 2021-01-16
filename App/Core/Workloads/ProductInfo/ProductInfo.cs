using LeoMongo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.ProductInfo
{
    public class ProductInfo : EntityBase
    {
        public string Name { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public List<Ingredient> Ingredients { get; set; } = default!;
    }
}
