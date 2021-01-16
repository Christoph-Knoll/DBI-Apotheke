using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Product;
using LeoMongo.Database;
using LeoMongo.Transaction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.ProductInfo
{
    public class ProductInfoRepository : GenericRepository<ProductInfo>, IProductInfoRepository
    {
        public ProductInfoRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
transactionProvider, databaseProvider)
        {
        }
    }
}
