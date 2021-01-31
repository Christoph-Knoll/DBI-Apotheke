using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Products;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Recipes
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        private readonly IProductRepository _productRepository;
        public RecipeRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider, IProductRepository productRepository) : base(
transactionProvider, databaseProvider)
        {
            this._productRepository = productRepository;
        }

        /*public Task<double> GetTotalPrice()
        {
            double totalPrice = 0;

            products.ForEach(p => totalPrice += p.Price);

            return totalPrice;
        }*/

        public async Task<double> GetTotalPrice(Recipe recipe)
        {
            List<Product> products = new List<Product>();
            Product product;
            double priceSum = 0.0;

            if (recipe.PZNs != null && this._productRepository != null)
            {
                /* recipe.PZNs.ForEach(async p =>
                 {
                     product = await _productRepository.GetByPzn(p);
                     products.Add(product);  
                 });*/
                int[] pzns = recipe.PZNs.ToArray();
                for (int i = 0; i < pzns.Length; i++)
                {
                    product = await _productRepository.GetByPzn(pzns[i]);
                    priceSum += product.Price;
                    products.Add(product);
                }
            }
            return priceSum;
        }
    }
}
