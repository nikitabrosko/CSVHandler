using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BL.Abstractions;
using BL.DataSourceParsers.FileParsersFactories;
using BL.FileManagers;
using BL.SalesDataSourceDTOs;
using DAL.Abstractions.UnitOfWorks;

namespace BL.ProcessManagers
{
    public class ProcessManager : IProcessManager
    {
        public event EventHandler<CompletionStateEventArgs> Completed;
        public event EventHandler<CompletionStateEventArgs> Failed;

        private IFileManager _fileManager;
        private readonly ISalesDbUnitOfWork _unitOfWork;
        private readonly string _sourceDirectoryPath;
        private readonly string _targetDirectoryPath;
        private readonly string _filesExtension;
        private readonly IList<string> _runningFiles = new List<string>();

        public ProcessManager(ISalesDbUnitOfWork unitOfWork, 
            string sourceDirectoryPath, 
            string targetDirectoryPath,
            string filesExtension)
        {
            _unitOfWork = unitOfWork;
            _sourceDirectoryPath = sourceDirectoryPath;
            _targetDirectoryPath = targetDirectoryPath;
            _filesExtension = filesExtension;
        }

        public async void RunAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                _fileManager = new FileManager(_sourceDirectoryPath, _filesExtension);

                FindAndHandleCurrentFiles();

                _fileManager.FileIsAdd += OnFileIsAdd;
            });
        }

        public void Run()
        {
            _fileManager = new FileManager(_sourceDirectoryPath, _filesExtension);

            FindAndHandleCurrentFiles();

            _fileManager.FileIsAdd += OnFileIsAdd;
        }

        public void Stop()
        {
            _fileManager.FileIsAdd -= OnFileIsAdd;
            _fileManager.Stop();
        }

        private async void FindAndHandleCurrentFiles()
        {
            var filesInDirectory = Directory.GetFiles(_sourceDirectoryPath, _filesExtension);

            if (filesInDirectory.Length != 0)
            {
                foreach (var file in filesInDirectory)
                {
                    await Task.Factory.StartNew(() => 
                        SaveDataAndMoveFiles(Path.GetFileName(file)));
                }
            }
        }

        private void SaveDataAndMoveFiles(string fileName)
        {
            try
            {
                if (!_runningFiles.Contains(fileName))
                {
                    _runningFiles.Add(fileName);

                    SaveDataToDatabase(ParseFile(fileName));
                    _fileManager.MoveFileToAnotherDirectory(_targetDirectoryPath, fileName);

                    _runningFiles.Remove(fileName);
                }
            }
            catch (Exception)
            {
                OnFailed(this, new CompletionStateEventArgs(CompletionState.Failed, fileName));
                throw;
            }
            finally
            {
                if (_runningFiles.Contains(fileName))
                {
                    _runningFiles.Remove(fileName);
                }
            }

            OnCompleted(this, new CompletionStateEventArgs(CompletionState.Completed, fileName));
        }

        private void OnFileIsAdd(object sender, FileSystemEventArgs args)
        {
            SaveDataAndMoveFiles(args.Name);
        }

        private void SaveDataToDatabase(ISalesDataSourceDTO dataToSave)
        {
            CustomerCheck();
            ManagerCheck();
            ProductCheck();

            _unitOfWork.OrderRepository.Insert(dataToSave.Order);

            _unitOfWork.Save();

            void CustomerCheck()
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

            void ManagerCheck()
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

            void ProductCheck()
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

        private ISalesDataSourceDTO ParseFile(string fileName)
        {
            try
            {
                using (var fileParser = new FileParserFactory().CreateInstance(_sourceDirectoryPath + fileName))
                {
                    var dataRaw = new SalesDataSourceDTOHandler(fileParser.ReadFile());

                    return dataRaw.TransformToSalesDataSourceDTO();
                }
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