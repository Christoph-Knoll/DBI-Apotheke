using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Modules;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using DBI_Apotheke.Core.Workloads.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Products
{
    public interface IProductService : IServiceBase<Product>
    {
        Task<Product> InsertItem(ProductInfo productInfo, int pzn, double price, int amount, Unit unit);

        Task<Product> GetByPzn(int pzn);

    }
}
