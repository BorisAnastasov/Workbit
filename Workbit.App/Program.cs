using LearnSpace.Web.Extensions;
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
                app.UseMigrationsEndPoint();
            }
            else
            {
                // Send unhandled exceptions to your custom 500 page
                app.UseExceptionHandler("/Error/Error500");

                // Redirect 404, 403, and other status codes to your custom pages
                app.UseStatusCodePagesWithReExecute("/Error/Error{0}");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.SeedRoles();

            app.Run();
        }
    }
}
