using LeoMongo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Products;

namespace DBI_Apotheke.Core.Workloads.ProductInfos
{
    public interface IProductInfoRepository : IGenericRepository<ProductInfo>
    {
        Task<IReadOnlyCollection<ProductInfo>> GetByIngredient(string ingredientName);

        Task<(ObjectId ItemId, List<ObjectId>? DetailIds)?> GetProductInfoWithProducts(ObjectId id);
    }
}
