using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Department;

namespace Workbit.App.Controllers
{
	public class DepartmentController : Controller
	{
        private readonly IDepartmentService departmentService;
        private readonly ICeoService ceoService;  // Assuming you need company info

        public DepartmentController(IDepartmentService _departmentService, ICeoService _ceoService)
        {
            departmentService = _departmentService;
            ceoService = _ceoService;
        }

        public async Task<IActionResult> AllDepartments()
		{
			var departments = await departmentService.GetAllByCeoIdAsync(User.Id());
			return View(departments);
		}

		[HttpGet]
		public async Task<IActionResult> DepartmentDetails(int id)
		{
			var department = await departmentService.GetByIdAsync(id); // Should return DepartmentReadDto

			return View(department);
		}

        [HttpGet]
        public async Task<IActionResult> CreateDepartment()
        {
            try
            {
                var ceo = await ceoService.GetByUserIdAsync(User.Id());

                var model = new DepartmentCreateDto
                {
                    CompanyId = ceo.CompanyId!.Value
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentCreateDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await departmentService.CreateAsync(model);

                TempData["Success"] = "Department created successfully!";
                return RedirectToAction("AllDepartments");
            }
            catch (Exception)
            {
                TempData["Error"] = $"Failed to create department!";
                return View(model);
            }
        }
    }
}

