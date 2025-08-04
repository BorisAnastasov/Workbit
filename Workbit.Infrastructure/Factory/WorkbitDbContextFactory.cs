using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Workbit.Infrastructure.Database;

namespace Workbit.Infrastructure.Factory
{
    public class WorkbitDbContextFactory : IDesignTimeDbContextFactory<WorkbitDbContext>
    {
        public WorkbitDbContext CreateDbContext(string[] args)
        {
            // Load configuration manually
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<WorkbitDbContext>();
            builder.UseSqlServer(connectionString);

            var dataProtection = DataProtectionProvider.Create("Workbit");
            return new WorkbitDbContext(builder.Options, dataProtection);
        }
    }
}
