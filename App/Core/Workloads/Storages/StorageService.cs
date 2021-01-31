using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Generics;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Storages
{
    public class StorageService : ServiceBase<Storage>, IStorageService
    {
        private readonly IStorageRepository _repository;


        public StorageService(IDateTimeProvider dateTimeProvider, IStorageRepository repository)
            :base(dateTimeProvider, repository)
        {
            _repository = repository;
        }

        public Task<Storage> InsertItem(int pzn, int amount, string storageSite)
        {
            var storage = new Storage
            {
                PZN = pzn,
                Amount = amount,
                StorageSite = storageSite
            };

            return this._repository.InsertItem(storage);
        }

        public Task<Storage> GetByPzn(int pzn) => _repository.GetByPzn(pzn);
    }
}
