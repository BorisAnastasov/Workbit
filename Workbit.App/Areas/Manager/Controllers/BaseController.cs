using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Workbit.Common.RoleConstants;

namespace Workbit.App.Areas.Manager.Controllers
{
    [Area(ManagerAreaName)]
    [Authorize(Roles =ManagerRoleName)]
    public class BaseController : Controller 
    {
        [HttpGet]
        public IActionResult NoDepartment()
        {
            return View();
        }
    }
}
