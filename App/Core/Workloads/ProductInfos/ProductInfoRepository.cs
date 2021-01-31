using DBI_Apotheke.Core.Workloads.Generics;
using LeoMongo.Database;
using LeoMongo.Transaction;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Workloads.Products;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DBI_Apotheke.Core.Workloads.ProductInfos
{
    public class ProductInfoRepository : GenericMasterDetailRepository<ProductInfo, Product>, IProductInfoRepository
    {
        public ProductInfoRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider, IProductRepository productRepository) 
            : base(transactionProvider, databaseProvider, productRepository)
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


        public Task<(ObjectId ItemId, List<ObjectId>? DetailIds)?> GetProductInfoWithProducts(ObjectId id)
        {
            return GetItemWithDetails(id, p => p.ProductInfoId);
        }

        public async Task<IReadOnlyCollection<ProductInfo>> GetByName(string name)
        {
            // A better approach would be to query using a BsonRegex-Expression
            // The Repository base would have to provide a Query(FilterDefinition<T> filter) function
            
            return (await Query().ToListAsync())
                .Where(x => x.Name.StartsWith(name))
                .ToImmutableList();
        }
        
    }
}
