using System;

namespace DAL.Abstractions.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsDisposed { get; }
        void Save();
    }
}