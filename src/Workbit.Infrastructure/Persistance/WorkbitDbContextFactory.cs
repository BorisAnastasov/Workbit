using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Workbit.Infrastructure.Security;

namespace Workbit.Infrastructure.Persistance;

public class WorkbitDbContextFactory : IDesignTimeDbContextFactory<WorkbitDbContext>
{
    public WorkbitDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WorkbitDbContext>();

        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=WorkbitDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");

        var services = new ServiceCollection();
        services.AddDataProtection();
        var serviceProvider = services.BuildServiceProvider();

        var dataProtectionProvider = serviceProvider.GetRequiredService<IDataProtectionProvider>();
        var encryptionService = new EncryptionService(dataProtectionProvider);

        return new WorkbitDbContext(optionsBuilder.Options, encryptionService);
    }
}