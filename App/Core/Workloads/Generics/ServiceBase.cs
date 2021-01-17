using DBI_Apotheke.Core.Util;
using LeoMongo.Database;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Generics
{
    public class ServiceBase<T> : IServiceBase<T>
        where T : EntityBase, new()
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IGenericRepository<T> _repository;


        public ServiceBase(IDateTimeProvider dateTimeProvider, IGenericRepository<T> repository)
        {
            this._dateTimeProvider = dateTimeProvider;
            this._repository = repository;
        }


        public virtual Task DeleteItem(ObjectId postId) => _repository.DeleteItem(postId);

        public virtual Task<IReadOnlyCollection<T>> GetAllItems() => _repository.GetAllItems();

        public virtual Task<T?> GetItemById(ObjectId id) => _repository.GetItemById(id);

        public virtual Task<IReadOnlyCollection<T>> Query(Predicate<T> query) => Query(query);

        public virtual Task<T> UpdateItem(T item) => _repository.UpdateItem(item);
    }
}
