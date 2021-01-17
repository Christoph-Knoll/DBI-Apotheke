using DBI_Apotheke.Core.Workloads.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.ProductInfos
{
    public class Ingredient
    {
        public string Name { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public Unit Unit { get; set; }
    }
}
