using System.Data.Common;
using System.Data.Entity;

namespace DAL.Abstractions
{
    public interface ISalesDbContextFactory
    {
        DbContext CreateInstance(DbConnection connection, bool ownConnection = true);
    }
}
