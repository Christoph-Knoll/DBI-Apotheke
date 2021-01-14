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
    public class GenericRepository<T> : RepositoryBase<T>, IGenericRepository<T> 
            where T : EntityBase, new()
    {
        public GenericRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
transactionProvider, databaseProvider)
        {
        }

        public override string CollectionName { get; } = MongoUtil.GetCollectionName<T>();

        #region CRUD
        public async Task<IReadOnlyCollection<T>> GetAllItems()
        {
            return await Query().ToListAsync();
        }

        public async Task<T?> GetItemById(ObjectId id)
        {
            return await base.Query().SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task DeleteItem(ObjectId postId)
        {
            await base.DeleteOneAsync(postId);
        }


        public async Task<T> InsertItem(T item)
        {
            return await base.InsertOneAsync(item);
        }

        public async Task<T> UpdateItem(T item)
        {
            await base.ReplaceOneAsync(item.Id, item);
            return await base.Query().SingleOrDefaultAsync(x => x.Id.Equals(item.Id));

        }
        #endregion

        public async Task<IReadOnlyCollection<T>> Query(Predicate<T> query)
        {
            return await base.Query().Where((x) => query(x)).ToListAsync();
        }
    }
}
