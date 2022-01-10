using System;

namespace BL.Abstractions
{
    public interface IProcessManager : IDisposable
    {
        event EventHandler<CompletionStateEventArgs> Completed;
        event EventHandler<CompletionStateEventArgs> Failed;
        void Run();
        void Stop();
    }
}