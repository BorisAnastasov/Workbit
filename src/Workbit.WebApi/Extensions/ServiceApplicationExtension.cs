using IbanNet.DependencyInjection.ServiceProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Workbit.Application.Common.Mappings;
using Workbit.Application.Common.Models;
using Workbit.Application.Interfaces;
using Workbit.Domain.Entities.Account;
using Workbit.Domain.Interfaces;
using Workbit.Infrastructure.Repository;
using Workbit.Infrastructure.Security;
using Workbit.Infrastructure.Services;

namespace Workbit.WebApi.Extensions
{
    public static class ServiceApplicationExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddControllers();

            services.AddSingleton<IEncryptionService, EncryptionService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher<ApplicationUser>, CustomPasswordHasherService>();
            services.AddScoped<IApiNinjasService, ApiNinjasService>();

            var redisConnectionString = config.GetConnectionString("Redis");
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "workbit_redis";
            });

            services.AddHybridCache(options =>
            {
                options.DefaultEntryOptions = new HybridCacheEntryOptions
                {
                    //L1
                    LocalCacheExpiration = TimeSpan.FromMinutes(2),
                    
                    //L2
                    Expiration = TimeSpan.FromMinutes(10)
                };
            });

            services.AddMediatR(configuration =>
            {
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

            var autoMapperKey = config.GetSection("AutoMapper:LicenseKey").Value;
            services.AddAutoMapper(cfg => {
                cfg.LicenseKey = autoMapperKey;
            },typeof(Program).Assembly,typeof(AuthProfile).Assembly);

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

            var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>()
                ?? throw new InvalidOperationException("JwtSettings section is missing in configuration.");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("access_token"))
                        {
                            context.Token = context.Request.Cookies["access_token"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
