using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DBI_Apotheke.Core.Workloads.ProductInfos
{
    public interface IProductInfoService : IServiceBase<ProductInfo>
    {
        Task<ProductInfo> InsertItem(string name, string brand, IEnumerable<Ingredient> ingredients);

        Task<IReadOnlyCollection<ProductInfo>> GetByIngredient(string ingredientName);

        Task<(ObjectId ItemId, List<ObjectId>? DetailIds)?> GetProductInfoWithProducts(ObjectId id);
    }
}
