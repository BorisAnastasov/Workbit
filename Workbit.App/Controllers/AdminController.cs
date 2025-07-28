using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Interfaces;
using Workbit.Core.Services;

namespace Workbit.App.Controllers
{
	public class AdminController : Controller
	{
		private readonly IAdminService adminService;

		public AdminController(IAdminService _adminService)
		{
			this.adminService = _adminService;
		}

		[HttpGet]
		public async Task<IActionResult> ManageUsers(
			string? search,
			string? role,
			string? department,
			string? sortBy,
			bool sortDesc = false)
		{
			// Fetch data via EmployeeService (handles filtering, sorting, role lookup via UserManager)
			var model = await adminService.GetAllUsersForAdminAsync(search, role, sortBy, sortDesc);

			// Keep selected filters so dropdowns show correct state
			model.SelectedRole = role ?? "All";
			model.SortBy = sortBy ?? "Name";
			model.SortDesc = sortDesc;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string id)
		{
			try
			{
				await adminService.DeleteUserAsync(id);
				return RedirectToAction(nameof(ManageUsers));

			}
			catch (Exception)
			{
				return RedirectToAction("Error500", "Error");
			}

		}
	}
}
