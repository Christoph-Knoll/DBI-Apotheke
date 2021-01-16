using DBI_Apotheke.Core.Workloads.Modules;
using LeoMongo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.ProductInfo
{
    public class Ingredient : EntityBase
    {
        public string Name { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public Unit Unit { get; set; }
    }
}
