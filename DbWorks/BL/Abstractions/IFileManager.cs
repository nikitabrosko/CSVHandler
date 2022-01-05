using System;
using System.IO;

namespace BL.Abstractions
{
    public interface IFileManager : IDisposable
    {
        event EventHandler<FileSystemEventArgs> FileIsAdd;
        void Stop();
        void MoveFileToAnotherDirectory(string targetDirectoryPath, string fileName);
    }
}