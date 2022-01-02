using System.Data.Entity;

namespace DAL.Abstractions
{
    public interface IRepositoryFactory
    {
        IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class;
    }
}