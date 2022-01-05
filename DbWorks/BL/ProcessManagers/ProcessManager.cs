using System;
using System.IO;
using System.Linq;
using BL.Abstractions;
using BL.DataSourceParsers.FileParsers;
using BL.DataSourceParsers.FileParsersFactories;
using BL.FileManagers;
using BL.SalesDataSourceDTOHandlers;
using DAL.Abstractions.UnitOfWorks;

namespace BL.ProcessManagers
{
    public class ProcessManager : IProcessManager
    {
        private IFileManager _fileManager;
        private readonly ISalesDbUnitOfWork _unitOfWork;
        private readonly string _sourceDirectoryPath;
        private readonly string _targetDirectoryPath;
        private readonly string _filesExtension;

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

        private void FindAndHandleCurrentFiles()
        {
            var filesInDirectory = Directory.GetFiles(_sourceDirectoryPath, _filesExtension);

            if (filesInDirectory.Length != 0)
            {
                foreach (var file in filesInDirectory)
                {
                    SaveDataAndMoveFiles(Path.GetFileName(file));
                }
            }
        }

        private void SaveDataAndMoveFiles(string fileName)
        {
            SaveDataToDatabase(ParseFile(fileName));
            _fileManager.MoveFileToAnotherDirectory(_targetDirectoryPath, fileName);
        }

        private void OnFileIsAdd(object sender, FileSystemEventArgs args)
        {
            SaveDataAndMoveFiles(args.Name);
        }

        private void SaveDataToDatabase(SalesDataSourceDTO dataToSave)
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

        private SalesDataSourceDTO ParseFile(string fileName)
        {
            var fileParser = new FileParserFactory().CreateInstance(_sourceDirectoryPath + fileName);

            var dataRaw = new SalesDataSourceDTOHandler(fileParser.ReadFile());

            fileParser.Dispose();

            return dataRaw.TransformToSalesDataSourceDTO();
        }

        public void Dispose()
        {
            Stop();
            _unitOfWork.Dispose();
            _fileManager.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}