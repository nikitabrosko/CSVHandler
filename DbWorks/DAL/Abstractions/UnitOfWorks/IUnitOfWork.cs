using System;

namespace DAL.Abstractions.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}