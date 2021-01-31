using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Products;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;

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

        public async Task<double> GetTotalPrice(ObjectId id)
        {
            var recipe = await GetItemById(id);

            var sum = 0.0;
            if (recipe?.PZNs != null)
            {
                foreach (var pzn in recipe.PZNs)
                {
                    var product = await _productRepository.GetByPzn(pzn);
                    
                    sum += product == null ? 0 : product.Price * product.Amount;
                }
            }
            return sum;
        }
    }
}
