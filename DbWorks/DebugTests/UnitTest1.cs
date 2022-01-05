using System.Data.SqlClient;
using System.IO;
using DAL.RepositoryFactories;
using DAL.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL.ProcessManagers;
using DAL.SalesDbContextFactories;

namespace DebugTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
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

            var processManager = new ProcessManager(salesDbUnitOfWork, 
                sourceDirectoryPath, targetDirectoryPath, filesExtension);

            processManager.Run();

            while (true)
            {
                
            }
        }
    }
}