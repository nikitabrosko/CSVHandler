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

        public bool IsDisposed { get; protected set; }
        
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
            var sourceFullPath = string.Concat(_directoryPath, fileName);
            var targetFullPath = string.Concat(targetDirectoryPath, fileName);

            if (File.Exists(targetFullPath))
            {
                File.Delete(targetFullPath);
            }

            File.Move(sourceFullPath, targetFullPath);
            File.Delete(sourceFullPath);
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