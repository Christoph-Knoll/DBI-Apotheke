using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.ProductInfos
{
    public interface IProductInfoService : IServiceBase<ProductInfo>
    {
        Task<ProductInfo> InsertItem(string name, string brand, IEnumerable<Ingredient> ingredients);

    }
}
