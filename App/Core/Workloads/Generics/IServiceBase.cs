using LeoMongo.Database;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Generics
{
    public interface IServiceBase<T> where T : EntityBase, new()
    {
        #region CRUD
        Task<T?> GetItemById(ObjectId id);
        Task<IReadOnlyCollection<T>> GetAllItems();
        //Task<T> InsertItem(T item);
        Task<T> UpdateItem(T item);
        Task DeleteItem(ObjectId postId);
        #endregion

        Task<IReadOnlyCollection<T>> Query(Predicate<T> query);
    }
}
