using DBI_Apotheke.Core.Workloads.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Products
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> GetByPzn(int pzn);
    }
}
