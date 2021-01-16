using DBI_Apotheke.Core.Workloads.Generics;
using LeoMongo.Database;
using LeoMongo.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Product
{
    public class StorageRepository : GenericRepository<Product>, IStorageRepository
    {
        public StorageRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
transactionProvider, databaseProvider)
        {
        }
    }
}
