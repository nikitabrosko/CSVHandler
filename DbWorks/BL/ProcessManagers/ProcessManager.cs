using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BL.Abstractions;
using BL.Abstractions.ConfigurationOptions;
using BL.DataSourceParsers.FileParsersFactories;
using BL.FileManagers;
using BL.SalesDataSourceDTOs;
using DAL.Abstractions.UnitOfWorks;

namespace BL.ProcessManagers
{
    public class ProcessManager : IProcessManager
    {
        private readonly Mutex _mutex = new Mutex();

        public event EventHandler<CompletionStateEventArgs> Completed;
        public event EventHandler<CompletionStateEventArgs> Failed;

        private IFileManager _fileManager;
        private readonly ISalesDbUnitOfWork _unitOfWork;
        private readonly IFileConfigurationOptions _fileConfiguration;

        public ProcessManager(ISalesDbUnitOfWork unitOfWork, IFileConfigurationOptions fileConfiguration)
        {
            Verify(unitOfWork, fileConfiguration);

            _unitOfWork = unitOfWork;
            _fileConfiguration = fileConfiguration;
        }

        private static void Verify(ISalesDbUnitOfWork unitOfWork, IFileConfigurationOptions fileConfiguration)
        {
            if (unitOfWork is null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (string.IsNullOrWhiteSpace(fileConfiguration.SourceDirectoryPath))
            {
                throw new ArgumentException("argument is null, empty or whitespace", 
                    nameof(fileConfiguration));
            }

            if (string.IsNullOrWhiteSpace(fileConfiguration.TargetDirectoryPath))
            {
                throw new ArgumentException("argument is null, empty or whitespace",
                    nameof(fileConfiguration));
            }

            if (string.IsNullOrWhiteSpace(fileConfiguration.FilesExtension))
            {
                throw new ArgumentException("argument is null, empty or whitespace",
                    nameof(fileConfiguration));
            }
        }

        public void Run()
        {
            _fileManager = new FileManager(_fileConfiguration.SourceDirectoryPath, 
                _fileConfiguration.FilesExtension);

            FindAndHandleCurrentFiles();

            _fileManager.FileIsAdd += OnFileIsAdd;
        }

        public void Stop()
        {
            _fileManager.FileIsAdd -= OnFileIsAdd;
            _fileManager.Stop();
        }

        private void FindAndHandleCurrentFiles()
        {
            var filesInDirectory = Directory.GetFiles(_fileConfiguration.SourceDirectoryPath, 
                _fileConfiguration.FilesExtension);

            if (filesInDirectory.Length != 0)
            {
                foreach (var file in filesInDirectory)
                {
                    Task.Factory.StartNew(() =>
                        SaveDataAndMoveFile(Path.GetFileName(file)));
                }
            }
        }

        private void SaveDataAndMoveFile(string fileName)
        {
            try
            {
                var data = ParseFile(fileName);

                _mutex.WaitOne();

                foreach (var salesData in data)
                {
                    SaveDataToDatabase(salesData);
                }

                _mutex.ReleaseMutex();
                _fileManager.MoveFileToAnotherDirectory(_fileConfiguration.TargetDirectoryPath, fileName);
            }
            catch (Exception)
            {
                OnFailed(this, new CompletionStateEventArgs(CompletionState.Failed, fileName));
                throw;
            }

            OnCompleted(this, new CompletionStateEventArgs(CompletionState.Completed, fileName));
        }

        private void OnFileIsAdd(object sender, FileSystemEventArgs args)
        {
            HandleFile(args.Name);
        }

        private void HandleFile(string fileName)
        {
            new Task(() => SaveDataAndMoveFile(fileName)).Start();
        }

        private void SaveDataToDatabase(SalesDataSourceDTO dataToSave)
        {
            CustomerCheckAndInsert();
            ManagerCheckAndInsert();
            ProductCheckAndInsert();

            _unitOfWork.OrderRepository.Insert(dataToSave.Order);

            _unitOfWork.Save();

            void CustomerCheckAndInsert()
            {
                if (!_unitOfWork.CustomerRepository.Get().Any(c => c.FirstName.Equals(dataToSave.Customer.FirstName)
                                                                   && c.LastName.Equals(dataToSave.Customer.LastName)))
                {
                    _unitOfWork.CustomerRepository.Insert(dataToSave.Customer);
                }
                else
                {
                    dataToSave.Order.Customer = _unitOfWork.CustomerRepository.Get()
                        .Single(c => c.FirstName.Equals(dataToSave.Customer.FirstName)
                                     && c.LastName.Equals(dataToSave.Customer.LastName));
                }
            }

            void ManagerCheckAndInsert()
            {
                if (!_unitOfWork.ManagerRepository.Get().Any(m => m.LastName.Equals(dataToSave.Manager.LastName)))
                {
                    _unitOfWork.ManagerRepository.Insert(dataToSave.Manager);
                }
                else
                {
                    dataToSave.Order.Manager = _unitOfWork.ManagerRepository.Get()
                        .Single(c => c.LastName.Equals(dataToSave.Manager.LastName));
                }
            }

            void ProductCheckAndInsert()
            {
                if (!_unitOfWork.ProductRepository.Get().Any(p => p.Name.Equals(dataToSave.Product.Name)
                                                                  && p.Price.Equals(dataToSave.Product.Price)))
                {
                    _unitOfWork.ProductRepository.Insert(dataToSave.Product);
                }
                else
                {
                    dataToSave.Order.Product = _unitOfWork.ProductRepository.Get()
                        .Single(c => c.Name.Equals(dataToSave.Product.Name)
                                     && c.Price.Equals(dataToSave.Product.Price));
                }
            }
        }

        private IEnumerable<SalesDataSourceDTO> ParseFile(string fileName)
        {
            try
            {
                var fileParser = new FileParserFactory()
                    .CreateInstance(_fileConfiguration.SourceDirectoryPath + fileName);

                return fileParser.ReadFile();
            }
            catch (IOException)
            {
                // Try to parse file again
                return ParseFile(fileName);
            }
        }

        public void Dispose()
        {
            Stop();
            _mutex.Dispose();
            _unitOfWork.Dispose();
            _fileManager.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void OnCompleted(object sender, CompletionStateEventArgs args)
        {
            Completed?.Invoke(sender, args);
        }

        protected virtual void OnFailed(object sender, CompletionStateEventArgs args)
        {
            Failed?.Invoke(sender, args);
        }
    }
}