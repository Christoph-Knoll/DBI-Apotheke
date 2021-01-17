using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Recipes
{
    public interface IRecipeRepository: IGenericRepository<Recipe>
    {
       double GetTotalPrice(List<Product> products);

    }

}
