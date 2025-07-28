using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Workbit.Common.RoleConstants;

namespace Workbit.App.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class BaseController : Controller {}
}
