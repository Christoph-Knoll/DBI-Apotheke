using LeoMongo;
using Microsoft.Extensions.Options;

namespace DBI_Apotheke.Core.Util
{
    public sealed class MongoConfig : IMongoConfig
    {
        public MongoConfig(IOptions<AppSettings> options)
        {
            var settings = options.Value;
            ConnectionString = settings.ConnectionString;
            DatabaseName = settings.Database;
        }
        public MongoConfig(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }
        public string ConnectionString { get; }
        public string DatabaseName { get; }
    }
}