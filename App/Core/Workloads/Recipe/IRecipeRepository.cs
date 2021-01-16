using DBI_Apotheke.Core.Workloads.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Recipe
{
    public interface IRecipeRepository: IGenericRepository<Recipe>
    {
    }
}
