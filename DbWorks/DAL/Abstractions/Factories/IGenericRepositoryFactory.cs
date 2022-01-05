using System.Data.Entity;

namespace DAL.Abstractions.Factories
{
    public interface IGenericRepositoryFactory
    {
        IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class;
    }
}