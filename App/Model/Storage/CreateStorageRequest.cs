using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Model.Storage
{
    public sealed class CreateStorageRequest
    {
        public int PZN { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public string StorageSite { get; set; } = default!;
    }
}
