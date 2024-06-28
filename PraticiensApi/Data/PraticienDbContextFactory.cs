using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PraticiensApi.Data
{
    public class PraticienDbContextFactory : IDesignTimeDbContextFactory<PraticienDb>
    {
        public PraticienDb CreateDbContext(string[] args)
        {
            // Configure the configuration builder to locate the appsettings.json file
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Retrieve the connection string from the configuration
            var builder = new DbContextOptionsBuilder<PraticienDb>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Ensure the connection string is not null or empty
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "The connection string cannot be null or empty.");
            }

            builder.UseSqlServer(connectionString);

            return new PraticienDb(builder.Options);
        }
    }
}