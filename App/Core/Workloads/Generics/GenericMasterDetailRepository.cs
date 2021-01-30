using DBI_Apotheke.Core.Util;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Generics
{
    public class GenericMasterDetailRepository<T, D> : GenericRepository<T>, IGenericMasterDetailRepository<T, D>
        where T : EntityBase, new()
        where D : EntityBase, new()
    {
        private readonly IGenericRepository<D> _detailRepository;

        public GenericMasterDetailRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
            IGenericRepository<D> detailRepository) : base(
            transactionProvider, databaseProvider)
        {
            this._detailRepository = detailRepository;
        }


        public async Task<(ObjectId ItemId, List<ObjectId>? DetailIds)?> GetItemWithDetails(ObjectId id, Func<D, ObjectId> foreignKeySelector)
        {
            IDictionary<ObjectId, List<ObjectId>?> itemWithDetails = await QueryIncludeDetail<D>(
        this._detailRepository,
        c => foreignKeySelector(c), p => p.Id == id)
    .ToDictionaryAsync();
            if (itemWithDetails.Count == 0
                || itemWithDetails.Keys.All(p => p != id))
            {
                return null;
            }

            return itemWithDetails.First().ToTuple();
        }
    }
}
