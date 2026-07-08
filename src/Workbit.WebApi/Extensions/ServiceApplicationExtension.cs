using IbanNet.DependencyInjection.ServiceProvider;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Workbit.Application.Interfaces;
using Workbit.Infrastructure.Security;

namespace Workbit.WebApi.Extensions
{
    public static class ServiceApplicationExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddControllers();

            services.AddSingleton<IEncryptionService, EncryptionService>();

            services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssembly(Assembly.Load("Workbit.Application"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowReact", policy =>
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            services.AddIbanNet();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

            return services;
        }
    }
}
