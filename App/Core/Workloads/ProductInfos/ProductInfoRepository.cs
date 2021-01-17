using DBI_Apotheke.Core.Workloads.Generics;
using LeoMongo.Database;
using LeoMongo.Transaction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DBI_Apotheke.Core.Workloads.ProductInfos
{
    public class ProductInfoRepository : GenericRepository<ProductInfo>, IProductInfoRepository
    {
        public ProductInfoRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
transactionProvider, databaseProvider)
        {
        }

        public async Task<IReadOnlyCollection<ProductInfo>> GetByIngredient(string ingredientName)
        {
            var productInfos = await Query().ToListAsync();
            List<ProductInfo> res = new List<ProductInfo>();
            foreach (var item in productInfos)
            {
                if(item.Ingredients.Exists(i => i.Name == ingredientName))
                    res.Add(item);
            }
            
            return (IReadOnlyCollection<ProductInfo>)res;
        }
    }
}
