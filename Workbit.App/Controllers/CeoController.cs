using Microsoft.AspNetCore.Mvc;

namespace Workbit.App.Controllers
{
    public class CeoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
