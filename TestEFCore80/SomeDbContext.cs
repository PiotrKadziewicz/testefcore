using Microsoft.Azure.Cosmos;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TestEFCore80
{
    public class SomeDbContext : DbContext
    {
        public DbSet<ValidationSchema> Validations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connString = "";//Azure Cosmos DB SQL API
            optionsBuilder.UseCosmos(connString!, databaseName: "testefcore");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("test");
            modelBuilder.ApplyConfiguration(new ValidationEntityConfiguration());
        }
    }
}
