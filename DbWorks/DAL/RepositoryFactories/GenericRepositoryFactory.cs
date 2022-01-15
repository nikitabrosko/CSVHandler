using DAL.Abstractions;
using DAL.Abstractions.Factories;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.RepositoryFactories
{
    public class GenericRepositoryFactory : IGenericRepositoryFactory
    {
        public IGenericRepository<TEntity> CreateInstance<TEntity>(DbContext context) where TEntity : class
        {
            return new GenericRepository<TEntity>(context);
        }
    }
}