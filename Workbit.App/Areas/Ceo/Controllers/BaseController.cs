using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Workbit.Common.RoleConstants;

namespace Workbit.App.Areas.Ceo.Controllers
{
    [Area(CeoAreaName)]
    [Authorize(Roles = CeoRoleName)]
    public class BaseController : Controller
    {
        public IActionResult NoCompany()
        {
            return View();
        }
    }
}
