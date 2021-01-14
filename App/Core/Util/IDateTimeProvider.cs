using System;

namespace DBI_Apotheke.Core.Util
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}