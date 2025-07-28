using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workbit.App.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error403()
        {
            return View();
        }
        public IActionResult Error500()
        {
            return View();
        }
    }
}
