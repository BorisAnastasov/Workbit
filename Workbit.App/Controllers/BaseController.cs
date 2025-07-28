using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workbit.App.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
