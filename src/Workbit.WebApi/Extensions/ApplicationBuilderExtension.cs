using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Workbit.Domain.Entities.Account;
using static Workbit.Domain.Constants.RoleConstants;

namespace Workbit.WebApi.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            var roles = new[] { AdminRoleName, ManagerRoleName, EmployeeRoleName, CeoRoleName };

            // 1) Ensure all roles exist and have a ClaimTypes.Role claim
            foreach (var role in roles)
            {
                var existingRole = await roleManager.FindByNameAsync(role);
                if (existingRole == null)
                {
                    var newRole = new IdentityRole<Guid>(role);
                    await roleManager.CreateAsync(newRole);
                    await roleManager.AddClaimAsync(newRole, new Claim(ClaimTypes.Role, role));
                }
                else
                {
                    var claims = await roleManager.GetClaimsAsync(existingRole);
                    if (!claims.Any(c => c.Type == ClaimTypes.Role && c.Value == role))
                    {
                        await roleManager.AddClaimAsync(existingRole, new Claim(ClaimTypes.Role, role));
                    }
                }
            }

            async Task EnsureUserRoleAsync(string email, string role)
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user != null && !await userManager.IsInRoleAsync(user, role))
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            await EnsureUserRoleAsync(AdminEmail, AdminRoleName);

            var employeeEmails = new[]
            {
                "alice.k.watson@workbit.com", "bob.c.thomas@workbit.com",
                "claire.b.james@workbit.com", "dave.r.walker@workbit.com",
                "emily.d.young@workbit.com", "frank.h.scott@workbit.com",
                "grace.l.adams@workbit.com", "harry.n.brooks@workbit.com"
            };
            foreach (var email in employeeEmails)
            {
                await EnsureUserRoleAsync(email, EmployeeRoleName);
            }

            var managerEmails = new[]
            {
                "lisa.r.anderson@workbit.com", "carl.t.morgan@workbit.com", "nina.v.hughes@workbit.com"
            };
            foreach (var email in managerEmails)
            {
                await EnsureUserRoleAsync(email, ManagerRoleName);
            }

            var ceoEmails = new[] { "john.m.lewis@workbit.com" };
            foreach (var email in ceoEmails)
            {
                await EnsureUserRoleAsync(email, CeoRoleName);
            }
        }
    }
}
