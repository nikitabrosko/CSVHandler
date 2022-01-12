using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace ServiceClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var hostBuilder = CreateHostBuilder(args).Build();

            try
            {
                hostBuilder.Run();
            }
            catch (Exception)
            {
                hostBuilder.Dispose();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", true, true);
                    builder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        true, true);
                })
                .ConfigureLogging(loggerFactory =>
                {
                    loggerFactory.AddEventLog(new EventLogSettings
                    {
                        LogName = "Sales Info Log",
                        SourceName = "SalesInfoService",
                        Filter = (message, level) => true
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .UseWindowsService();
    }
}
