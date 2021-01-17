using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Recipes
{
    public class RecipeService : ServiceBase<Recipe>, IRecipeService
    {
        private readonly IRecipeRepository _repository;
        public RecipeService(IDateTimeProvider dateTimeProvider, IRecipeRepository repository)
            : base(dateTimeProvider, repository)
        { 
            _repository = repository;
        }
        public Task<Recipe> InsertItem(string name, string address, string issuer, List<int> pzns)
        {
            var recipe = new Recipe
            {
                PZNs = pzns,
                Name = name,
                Address = address,
                Issuer = issuer
            };

            return this._repository.InsertItem(recipe);
        }
    }
}
