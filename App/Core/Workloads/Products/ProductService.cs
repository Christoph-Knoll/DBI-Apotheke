using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.Modules;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Products
{
    public class ProductService : ServiceBase<Product>, IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IDateTimeProvider dateTimeProvider, IProductRepository repository)
            : base(dateTimeProvider, repository)
        {
            _repository = repository;
        }

        public Task<Product> InsertItem(ProductInfo productInfo, int pzn, double price, int amount, Unit unit)
        {
            var product = new Product
            {
                ProductInfoId = productInfo.Id,
                PZN = pzn,
                Price = price,
                Amount = amount,
                Unit = unit
            };

            return this._repository.InsertItem(product);
        }
        
        public Task<Product> GetByPzn(int pzn) => _repository.GetByPzn(pzn);

        public Task<IReadOnlyCollection<Product>> GetAllProductsByProductInfo(ObjectId productInfoId)
        {
            return this._repository.GetAllProductsByProductInfo(productInfoId);
        }
    }
}
