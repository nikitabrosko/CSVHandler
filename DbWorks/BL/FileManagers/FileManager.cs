using System;
using System.IO;
using BL.Abstractions;

namespace BL.FileManagers
{
    public class FileManager : IFileManager
    {
        public event EventHandler<FileSystemEventArgs> FileIsAdd;

        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly string _directoryPath;

        public bool IsDisposed;
        
        public FileManager(string directoryPath, string filesToWatchExtension)
        {
            _directoryPath = directoryPath;

            _fileSystemWatcher = new FileSystemWatcher(directoryPath)
            {
                Filter = filesToWatchExtension
            };

            _fileSystemWatcher.Created += OnCreated;

            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _fileSystemWatcher.Created -= OnCreated;
        }

        public void MoveFileToAnotherDirectory(string targetDirectoryPath, string fileName)
        {
            File.Move(_directoryPath + fileName, targetDirectoryPath + fileName);
            File.Delete(_directoryPath + fileName);
        }

        protected virtual void OnCreated(object sender, FileSystemEventArgs args)
        {
            FileIsAdd?.Invoke(this, args);
        }
        
        public void Dispose()
        {
            _fileSystemWatcher.Created -= OnCreated;
            _fileSystemWatcher?.Dispose();
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}