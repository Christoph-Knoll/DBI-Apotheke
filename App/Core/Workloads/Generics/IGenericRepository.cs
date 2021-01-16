using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Generics
{
    public interface IGenericRepository<T> : IRepositoryBase where T : EntityBase, new()
    {
        #region CRUD
        Task<T?> GetItemById(ObjectId id);
        Task<IReadOnlyCollection<T>> GetAllItems();
        Task<T> InsertItem(T item);
        Task<T> UpdateItem(T item);
        Task DeleteItem(ObjectId postId); 
        #endregion

        Task<IReadOnlyCollection<T>> Query(Predicate<T> query);

        //Task<(ObjectId ItemId, List<ObjectId>? DetailIds)> GetItemWithDetails(ObjectId id);

    }
}