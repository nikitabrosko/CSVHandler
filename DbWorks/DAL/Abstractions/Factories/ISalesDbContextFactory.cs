using DatabaseLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Abstractions.Factories
{
    public interface ISalesDbContextFactory
    {
        DbContext CreateInstance(DbContextOptions<SalesDbContext> options);
    }
}
