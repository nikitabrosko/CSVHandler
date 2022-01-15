using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using BL.Abstractions;

namespace ServiceClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IProcessManager _processManager;

        public Worker(IProcessManager processManager, ILogger<Worker> logger)
        {
            _processManager = processManager;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service starting...");

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
    }
}
