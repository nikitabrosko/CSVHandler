using DAL.Abstractions.Factories;
using DatabaseLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DAL.SalesDbContextFactories
{
    public class SalesDbContextFactory : ISalesDbContextFactory
    {
        public DbContext CreateInstance(DbContextOptions<SalesDbContext> options)
        {
            return new SalesDbContext(options);
        }
    }
}