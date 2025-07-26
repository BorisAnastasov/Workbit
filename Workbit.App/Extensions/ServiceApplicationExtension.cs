using LearnSpace.Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Services;
using Workbit.Infrastructure.Database;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.App.Extensions
{
	public static class ServiceApplicationExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IAttendanceService, AttendanceService>();
			services.AddScoped<ICeoService, CeoService>();
			services.AddScoped<ICompanyService, CompanyService>();
			services.AddScoped<IDepartmentService, DepartmentService>();
			services.AddScoped<IEmployeeService, EmployeeService>();
			services.AddScoped<IJobService, JobService>();
			services.AddScoped<IManagerService, ManagerService>();
			services.AddScoped<IPaymentService, PaymentService>();

			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.SameSite = SameSiteMode.Lax;  // Avoid Strict for redirects
				options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Allow HTTP during development
				options.ExpireTimeSpan = TimeSpan.FromHours(1);
				options.LoginPath = "/User/Login";
				options.LogoutPath = "/User/Logout";
				options.AccessDeniedPath = "/Account/AccessDenied";
			});

			return services;
		}
		public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
		{
			var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<WorkbitDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
				options.UseLazyLoadingProxies()
							  .UseSqlServer(connectionString);
			});

			services.AddScoped<IRepository, Repository>();

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
}
