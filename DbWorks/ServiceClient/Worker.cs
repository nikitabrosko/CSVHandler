using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BL.Abstractions;
using BL.ProcessManagers;
using DAL.RepositoryFactories;
using DAL.SalesDbContextFactories;
using DAL.UnitOfWorks;
using Microsoft.Extensions.Configuration;

namespace ServiceClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IProcessManager _processManager;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service starting...");

            StartApp();

            _processManager.Completed += (sender, args) =>
                _logger.LogInformation($"File {args.FileName} is successfully handled!");
            _processManager.Failed += (sender, args) =>
                _logger.LogError($"File {args.FileName} handle is failed!");

            var task = new Task(_processManager.Run);
            task.Start();

            _logger.LogInformation("Service is successfully started!");

            await task;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopped!");
            _processManager.Dispose();

            return Task.CompletedTask;
        }

        private void StartApp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"appsettings.json"))
                .Build();

            var sourceDirectoryPath = Path.GetFullPath(config.GetSection("AppOptions:FolderOptions:Source").Value);
            var targetDirectoryPath = Path.GetFullPath(config.GetSection("AppOptions:FolderOptions:Target").Value);
            var filesExtension = config.GetSection("AppOptions:FolderOptions:Extension").Value;
            var connectionString = config.GetSection("AppOptions:ConnectionOptions:Default").Value;

            var salesDbContext = new SalesDbContextFactory().CreateInstance(new SqlConnection(connectionString));
            var salesDbUnitOfWork = new SalesDbUnitOfWork(salesDbContext, new GenericRepositoryFactory());

            _processManager = new ProcessManager(salesDbUnitOfWork,
                sourceDirectoryPath, targetDirectoryPath, filesExtension);
        }
    }
}
