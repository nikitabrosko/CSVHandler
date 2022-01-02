using System.Data.Entity;
using DAL.Abstractions;
using DAL.Repositories;

namespace DAL.RepositoryFactories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class
        {
            return new GenericRepository<TEntity>(context);
        }
    }
}