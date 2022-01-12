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
            Verify(directoryPath, filesToWatchExtension);

            _directoryPath = directoryPath;

            _fileSystemWatcher = new FileSystemWatcher(directoryPath)
            {
                Filter = filesToWatchExtension
            };

            Start();
        }

        private static void Verify(string directoryPath, string filesToWatchExtension)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("argument is null, empty or whitespace",
                    nameof(directoryPath));
            }

            if (string.IsNullOrWhiteSpace(filesToWatchExtension))
            {
                throw new ArgumentException("argument is null, empty or whitespace",
                    nameof(filesToWatchExtension));
            }
        }

        public void Start()
        {
            _fileSystemWatcher.Created += OnCreated;

            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _fileSystemWatcher.Created -= OnCreated;

            _fileSystemWatcher.EnableRaisingEvents = false;
        }

        public void MoveFileToAnotherDirectory(string targetDirectoryPath, string fileName)
        {
            if (!Directory.Exists(targetDirectoryPath))
            {
                Directory.CreateDirectory(targetDirectoryPath);
            }

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
            Stop();
            _fileSystemWatcher?.Dispose();
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}