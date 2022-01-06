using System;
using System.Data.SqlClient;
using System.IO;
using BL.Abstractions;
using BL.ProcessManagers;
using DAL.RepositoryFactories;
using DAL.SalesDbContextFactories;
using DAL.UnitOfWorks;
using Microsoft.Extensions.Configuration;

namespace ConsoleClient
{
    public class ConsoleApp
    {
        private ProcessManager _processManager;

        public void Start()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\\..\\..\\appsettings.json"))
                .Build();

            var sourceDirectoryPath = Path.GetFullPath(config.GetSection("AppOptions:FolderOptions:Source").Value);
            var targetDirectoryPath = Path.GetFullPath(config.GetSection("AppOptions:FolderOptions:Target").Value);
            var filesExtension = config.GetSection("AppOptions:FolderOptions:Extension").Value;
            var connectionString = config.GetSection("AppOptions:ConnectionOptions:Default").Value;

            var salesDbContext = new SalesDbContextFactory().CreateInstance(new SqlConnection(connectionString));
            var salesDbUnitOfWork = new SalesDbUnitOfWork(salesDbContext, new GenericRepositoryFactory());

            _processManager = new ProcessManager(salesDbUnitOfWork,
                sourceDirectoryPath, targetDirectoryPath, filesExtension);

            Configure();

            try
            {
                Console.WriteLine("Listening...");
                _processManager.RunAsync();
            }
            catch (Exception)
            {
                Stop();
            }
        }

        public void Stop()
        {
            _processManager.Completed -= OnCompletionEventInvoked;
            _processManager.Failed -= OnCompletionEventInvoked;
            _processManager.Stop();
            _processManager.Dispose();
            Console.WriteLine("Application is stopped");
        }

        public void Configure()
        {
            _processManager.Completed += OnCompletionEventInvoked;
            _processManager.Failed += OnCompletionEventInvoked;
        }

        protected virtual void OnCompletionEventInvoked(object sender, CompletionStateEventArgs args)
        {
            switch (args.CompletionState)
            {
                case CompletionState.Completed:
                    Console.WriteLine($"File {args.FileName} is successfully handled!");
                    break;
                case CompletionState.Failed:
                    Console.WriteLine($"File {args.FileName} handle is failed!");
                    break;
            }
        }
    }
}