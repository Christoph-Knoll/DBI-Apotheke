using DBI_Apotheke.Core.Workloads.Modules;
using LeoMongo.Database;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Products
{
    public sealed class Product : EntityBase
    {
        public int PZN { get; set; } = default!;
        public double Price { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public Unit Unit { get; set; }
        public ObjectId ProductInfoId { get; set; }
    }
}
