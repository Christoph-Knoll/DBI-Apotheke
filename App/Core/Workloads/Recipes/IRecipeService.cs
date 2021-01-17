using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Modules;
using DBI_Apotheke.Core.Workloads.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Recipes
{
    public interface IRecipeService : IServiceBase<Recipe>
    {
        Task<Recipe> InsertItem(string name, string address, string issuer, List<int> pzns);
    }
}
