using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Model.Recipe
{
    public class RecipeDTO
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public List<int> PZNs { get; set; } = default!;
        public double PriceSum { get; set; } = default;
    }
}