using LeoMongo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using DBI_Apotheke.Core.Workloads.Generics;

namespace DBI_Apotheke.Core.Workloads.ProductInfo
{
    public interface IProductInfoRepository : IGenericRepository<ProductInfo>
    {
    }
}
