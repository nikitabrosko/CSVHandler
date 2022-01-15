using System;
using System.IO;
using BL.Abstractions;
using BL.ConfigurationOptions;
using BL.ProcessManagers;
using DAL.RepositoryFactories;
using DAL.UnitOfWorks;
using DatabaseLayer.Contexts;
using Microsoft.EntityFrameworkCore;
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
                .ConfigureServices(services =>
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonFile(Path.GetFullPath(@"appsettings.json"))
                        .Build();

                    var fileConfiguration = new FileConfigurationOptions();

                    config.Bind("AppOptions:FolderOptions", fileConfiguration);
                    
                    var connectionString = config.GetSection("ConnectionStrings:Default").Value;


                    services.AddHostedService<Worker>();
                    services.AddSingleton<IProcessManager, ProcessManager>(serviceProvider =>
                    {
                        var contextOptions = new DbContextOptionsBuilder<SalesDbContext>()
                            .UseSqlServer(connectionString).Options;
                        var salesDbContext = new SalesDbContext(contextOptions);
                        var salesDbUnitOfWork = new SalesDbUnitOfWork(salesDbContext, new GenericRepositoryFactory());

                        return new ProcessManager(salesDbUnitOfWork, fileConfiguration);
                    });
                })
                .UseWindowsService();
    }
}
