using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using DbWorks.Models;
using Microsoft.Extensions.Configuration;

namespace DbWorks.Contexts
{
    public class SalesDbContext : DbContext
    {
        private readonly DbConnection _connection;
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public SalesDbContext(DbConnection connection, bool ownConnection) 
            : base(connection, ownConnection)
        {
            _connection = connection;

            //Database.Delete();

            if (!Database.Exists())
            {
                Database.Create();
                AddTriggersToDatabase();
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.FirstName, c.LastName }).IsUnique();
            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Customer>()
                .Property(c => c.FullName).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders).WithRequired(o => o.Customer);

            modelBuilder.Entity<Manager>()
                .HasIndex(m => new { m.LastName }).IsUnique();
            modelBuilder.Entity<Manager>()
                .Property(m => m.LastName).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Orders).WithRequired(o => o.Manager);

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Name }).IsUnique();
            modelBuilder.Entity<Product>()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders).WithRequired(o => o.Product);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id).Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Order>()
                .Property(o => o.Date).IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.Sum).IsRequired();
        }

        private void AddTriggersToDatabase()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\\..\\..\\..\\DbWorks\\appTriggersSettings.json"))
                .Build();

            Database.ExecuteSqlCommand(File.ReadAllText(config.GetSection("TriggersFolders:CustomerFullName").Value));
        }
    }
}