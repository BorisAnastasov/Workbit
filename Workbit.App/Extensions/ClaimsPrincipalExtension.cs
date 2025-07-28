using System.Security.Claims;
using Workbit.Infrastructure.Database;
using static Workbit.Common.RoleConstants;

namespace Workbit.App.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsUser(this ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated;
        }
        public static bool IsManager(this ClaimsPrincipal user)
        {

            return user.IsInRole(ManagerRoleName);
        }

        public static bool IsEmployee(this ClaimsPrincipal user)
        {
            return user.IsInRole(EmployeeRoleName);
        }

        public static bool IsCeo(this ClaimsPrincipal user)
        {
            return user.IsInRole(CeoRoleName);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
