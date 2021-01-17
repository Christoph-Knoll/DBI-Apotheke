using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Generics
{
    public interface IGenericMasterDetailRepository<T, D> : IGenericRepository<T> 
        where T : EntityBase, new()
        where D : EntityBase, new()
    {

        Task<(ObjectId ItemId, List<ObjectId>? DetailIds)?> GetItemWithDetails(ObjectId id, Func<D, ObjectId> foreignKeySelector);

    }
}