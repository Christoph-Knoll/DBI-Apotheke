using LeoMongo.Database;
using LeoMongo.Transaction;

namespace LeoMongo
{
    public class Factory
    {
        public static IDatabaseProvider CreateDatabaseProvider(IMongoConfig options)
        {
            return new DatabaseProvider(options);
        }

        public static ITransactionProvider CreateTransactionProvider(IDatabaseProvider databaseProvider)
        {
            return new TransactionProvider(databaseProvider);
        }
    }
}