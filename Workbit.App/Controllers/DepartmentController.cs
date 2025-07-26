using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;

namespace Workbit.App.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentService _departmentService;

		public DepartmentController(IDepartmentService departmentService)
		{
			_departmentService = departmentService;
		}

		public async Task<IActionResult> AllDepartments()
		{
			var departments = await _departmentService.GetAllByCeoIdAsync(User.Id());
			return View(departments);
		}

		[HttpGet]
		public async Task<IActionResult> DepartmentInfo(int id)
		{
			var department = await _departmentService.GetByIdAsync(id); // Should return DepartmentReadDto

			return View(department);
		}
	}
}
