using DBI_Apotheke.Core.Workloads.Modules;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Model.Product
{
    public sealed class CreateProductRequest
    {
        public int PZN { get; set; } = default!;
        public double Price { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public Unit Unit { get; set; }
        public string ProductInfoId { get; set; }
    }
}
