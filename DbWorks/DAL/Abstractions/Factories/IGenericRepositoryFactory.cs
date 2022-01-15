using Microsoft.EntityFrameworkCore;

namespace DAL.Abstractions.Factories
{
    public interface IGenericRepositoryFactory
    {
        IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class;
    }
}