using LeoMongo.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Core.Workloads.Storages
{
    public sealed class Storage : EntityBase
    {
        public int PZN { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public string StorageSite { get; set; } = default!;
    }
}
