using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Workbit.Common.RoleConstants;
namespace Workbit.App.Areas.Employee.Controllers
{
    [Area(EmployeeAreaName)]
    [Authorize(Roles = EmployeeRoleName)]
    public class BaseController : Controller {}
}
