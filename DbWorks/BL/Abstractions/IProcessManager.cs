using System;

namespace BL.Abstractions
{
    public interface IProcessManager : IDisposable
    {
        void Run();
        void Stop();
    }
}