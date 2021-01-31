using DBI_Apotheke.Core.Workloads.Generics;
using LeoMongo.Database;
using LeoMongo.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace DBI_Apotheke.Core.Workloads.Storages
{
    public class StorageRepository : GenericRepository<Storage>, IStorageRepository
    {
        public StorageRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
transactionProvider, databaseProvider)
        {
        }
        
        public Task<Storage> GetByPzn(int pzn)
        {
            return this.Query().SingleOrDefaultAsync(x => x.PZN == pzn);
        }
    }
}
