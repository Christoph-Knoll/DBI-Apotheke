﻿using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DBI_Apotheke.Core.Workloads.ProductInfos
{
    public class ProductInfoService : ServiceBase<ProductInfo>, IProductInfoService
    {
        private readonly IProductInfoRepository _repository;

        public ProductInfoService(IDateTimeProvider dateTimeProvider, IProductInfoRepository repository)
            : base(dateTimeProvider, repository)
        {
            _repository = repository;
        }

        public Task<ProductInfo> InsertItem(string name, string brand, IEnumerable<Ingredient> ingredients)
        {
            var productInfo = new ProductInfo
            {
                Name = name,
                Brand = brand,
                Ingredients = ingredients.ToList()
            };

            return this._repository.InsertItem(productInfo);
        }

        public Task<IReadOnlyCollection<ProductInfo>> GetByIngredient(string ingredientName) =>
            _repository.GetByIngredient(ingredientName);

        public Task<(ObjectId ItemId, List<ObjectId>? DetailIds)?> GetProductInfoWithProducts(ObjectId id) => 
            _repository.GetProductInfoWithProducts(id);

        public Task<IReadOnlyCollection<ProductInfo>> GetByName(string name) =>
            _repository.GetByName(name);
    }
}
