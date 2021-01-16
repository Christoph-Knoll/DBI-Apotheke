using DBI_Apotheke.Core.Workloads.Generics;
using LeoMongo.Database;
using LeoMongo.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Storage
{
    public class RecipeRepository : GenericRepository<Storage>, IRecipeRepository
    {
        public RecipeRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
transactionProvider, databaseProvider)
        {
        }
    }
}
