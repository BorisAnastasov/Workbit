using Microsoft.AspNetCore.Identity;
using Workbit.Infrastructure.Database.Entities.Account;
using static Workbit.Common.RoleConstants;

namespace LearnSpace.Web.Extensions
{
	public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async ()=> 
            {
                var roles = new[] { AdminRoleName, ManagerRoleName, EmployeeRoleName };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    }
                }

                var admin = await userManager.FindByEmailAsync(AdminEmail);

                await userManager.AddToRoleAsync(admin, AdminRoleName);


                var employeeEmails = new[] { "student1@abv.bg", "student2@abv.bg", "student3@abv.bg", "student4@abv.bg", "student5@abv.bg" };

                foreach (var email in employeeEmails)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if (user != null && await roleManager.RoleExistsAsync(EmployeeRoleName))
                    {
                        await userManager.AddToRoleAsync(user, EmployeeRoleName);
                    }
                }

                var managerEmails = new[] { "teacher1@abv.bg", "teacher2@abv.bg" };

                foreach (var email in managerEmails)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if (user != null && await roleManager.RoleExistsAsync(ManagerRoleName))
                    {
                        await userManager.AddToRoleAsync(user, ManagerRoleName);
                    }
                }
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
