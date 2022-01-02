using System;
using System.Data.Common;
using System.Data.Entity;
using DbWorks.Contexts;

namespace DAL.SalesDbContextFactory
{
    public class SalesDbContextFactory : ISalesDbContextFactory
    {
        public DbContext CreateInstance(DbConnection connection, bool ownConnection = true)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            return new SalesDbContext(connection, ownConnection);
        }
    }
}