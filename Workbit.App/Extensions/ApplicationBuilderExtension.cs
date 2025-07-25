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
                var roles = new[] { AdminRoleName, ManagerRoleName, EmployeeRoleName, CeoRoleName };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    }
                }

                var admin = await userManager.FindByEmailAsync(AdminEmail);

                await userManager.AddToRoleAsync(admin, AdminRoleName);


                var employeeEmails = new[] { "alice.k.watson@workbit.com", "bob.c.thomas@workbit.com", "claire.b.james@workbit.com", "dave.r.walker@workbit.com",
                                                "emily.d.young@workbit.com", "frank.h.scott@workbit.com", "grace.l.adams@workbit.com", "harry.n.brooks@workbit.com"};

                foreach (var email in employeeEmails)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if (user != null && await roleManager.RoleExistsAsync(EmployeeRoleName))
                    {
                        await userManager.AddToRoleAsync(user, EmployeeRoleName);
                    }
                }

                var managerEmails = new[] { "john.m.lewis@workbit.com", "lisa.r.anderson@workbit.com", "carl.t.morgan@workbit.com", "nina.v.hughes@workbit.com" };

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
