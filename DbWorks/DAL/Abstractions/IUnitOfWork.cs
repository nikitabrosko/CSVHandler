using System;

namespace DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}