using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workbit.Domain.Entities.Account;
using Workbit.Domain.Interface;
using Workbit.Infrastructure.Database.Repository;
using Workbit.Infrastructure.Persistance;

namespace Workbit.Infrastructure.Extensions;

public static class ServiceInfrastructureExtension
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<WorkbitDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }

    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddEntityFrameworkStores<WorkbitDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}

