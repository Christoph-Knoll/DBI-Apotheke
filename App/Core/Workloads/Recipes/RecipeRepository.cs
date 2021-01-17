using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Products;
using LeoMongo.Database;
using LeoMongo.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Recipes
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
transactionProvider, databaseProvider)
        {
        }

        public double GetTotalPrice(List<Product> products)
        {
            double totalPrice = 0;

            products.ForEach(p => totalPrice += p.Price);

            return totalPrice;
        }


    }
}
