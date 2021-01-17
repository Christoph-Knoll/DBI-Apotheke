using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Workloads.ProductInfos;

namespace DBI_Apotheke.Model.ProductInfos
{
    public class ProductInfoDTO
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public List<Ingredient> Ingredients { get; set; } = default!;
    }
}
