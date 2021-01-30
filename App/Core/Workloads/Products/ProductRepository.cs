using DBI_Apotheke.Core.Workloads.Generics;
using LeoMongo.Database;
using LeoMongo.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DBI_Apotheke.Core.Workloads.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
transactionProvider, databaseProvider)
        {
        }

        public Task<Product> GetByPzn(int pzn)
        {
            return this.Query().SingleOrDefaultAsync(x => x.PZN == pzn);
        }

        public async Task<IReadOnlyCollection<Product>> GetAllProductsByProductInfo(ObjectId productInfoId)
        {
            /* var query = from p in collection.AsQueryable()
                         join o in otherCollection on p.Name equals o.Key into joined
                         select new { p.Name, AgeSum: joined.Sum(x => x.Age) };*/

            return await Query().Where(p => p.ProductInfoId == productInfoId).ToListAsync();
        }

    }
}
