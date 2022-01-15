using System;
using System.IO;
using BL.Abstractions;
using BL.ConfigurationOptions;
using BL.ProcessManagers;
using DAL.RepositoryFactories;
using DAL.SalesDbContextFactories;
using DAL.UnitOfWorks;
using DatabaseLayer.Contexts;
using Microsoft.EntityFrameworkCore;
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

            var fileConfiguration = new FileConfigurationOptions();

            config.Bind("AppOptions:FolderOptions", fileConfiguration);

            var connectionString = config.GetSection("ConnectionStrings:Default").Value;

            var contextOptions = new DbContextOptionsBuilder<SalesDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var salesDbContext = new SalesDbContextFactory()
                .CreateInstance(contextOptions);
            var salesDbUnitOfWork = new SalesDbUnitOfWork(salesDbContext, new GenericRepositoryFactory());

            _processManager = new ProcessManager(salesDbUnitOfWork, fileConfiguration);

            Configure();

            try
            {
                Console.WriteLine("Listening...");
                _processManager.Run();
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