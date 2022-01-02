using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using DbWorks.Models;

namespace DbWorks.Contexts
{
    public partial class SalesDbContext : DbContext
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
            Database.Delete();
            Database.Create();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id).Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Customer>()
                .Property(c => c.FullName).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Products);

            modelBuilder.Entity<Manager>()
                .HasKey(m => m.Id).Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Manager>()
                .Property(m => m.LastName).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Orders);
            
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id).Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Order>()
                .Property(o => o.Date).IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.Sum).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Customer);
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Manager);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products);
            
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).IsRequired();
        }
    }
}