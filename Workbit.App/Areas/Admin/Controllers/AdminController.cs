using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Interfaces;

namespace Workbit.App.Areas.Admin.Controllers
{

    public class AdminController : BaseController
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService _adminService)
        {
            adminService = _adminService;
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers(
            string? search,
            string? role = "All")
        {
            var model = await adminService.GetAllUsersForAdminAsync(search, role);


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                if (!await adminService.UserExistsById(id)) 
                {
					return RedirectToAction("Error404", "Error");
				}
                await adminService.DeleteUserAsync(id);

                return RedirectToAction(nameof(AllUsers));

            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }

        }

		[HttpGet]
		public async Task<IActionResult> UserDetails(string id)
		{

			if (!await adminService.UserExistsById(id))
			{
                return NotFound();
;			}

			var model = await adminService.GetUserDetailsAsync(id);

			return View(model);
		}
	}
}
