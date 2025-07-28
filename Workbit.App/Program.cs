using Workbit.Web.Extensions;
using Workbit.App.Extensions;

namespace Workbit.App
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddApplicationDbContext(builder.Configuration);

			builder.Services.AddApplicationIdentity(builder.Configuration);

			builder.Services.AddApplicationServices();

			builder.Services.AddControllersWithViews();

			builder.Services.AddRazorPages();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				app.UseExceptionHandler("/Error/Error500");

				app.UseStatusCodePagesWithReExecute("/Error/Error{0}");
			}
			else
			{
				app.UseExceptionHandler("/Error/Error500");

				app.UseStatusCodePagesWithReExecute("/Error/Error{0}");

				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
					name: "areas",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
					);

			app.MapDefaultControllerRoute();

			app.SeedRoles();

			app.Run();
		}
	}
}
