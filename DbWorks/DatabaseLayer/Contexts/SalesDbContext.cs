using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Contexts
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.FirstName, c.LastName }).IsUnique();
            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Customer>()
                .Property(c => c.FullName).HasComputedColumnSql("[LastName] + ' ' + [FirstName]");
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders).WithOne(o => o.Customer);

            modelBuilder.Entity<Manager>()
                .HasIndex(m => new { m.LastName }).IsUnique();
            modelBuilder.Entity<Manager>()
                .Property(m => m.LastName).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Orders).WithOne(o => o.Manager);

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Name }).IsUnique();
            modelBuilder.Entity<Product>()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders).WithOne(o => o.Product);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Order>()
                .Property(o => o.Date).IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.Sum).IsRequired();
        }
    }
}