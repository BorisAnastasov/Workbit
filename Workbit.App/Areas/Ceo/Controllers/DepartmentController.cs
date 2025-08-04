using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Department;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService departmentService;
        private readonly ICeoService ceoService;  // Assuming you need company info

        public DepartmentController(IDepartmentService _departmentService, ICeoService _ceoService)
        {
            departmentService = _departmentService;
            ceoService = _ceoService;
        }

        public async Task<IActionResult> All()
        {
            var departments = await departmentService.GetAllByCeoIdAsync(User.Id());
            return View(departments);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var department = await departmentService.GetByIdAsync(id); // Should return DepartmentReadDto

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var ceo = await ceoService.GetByUserIdAsync(User.Id());

                var model = new DepartmentCreateFormModel
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
        public async Task<IActionResult> Create(DepartmentCreateFormModel model)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await departmentService.DeleteDepartmentAsync(id);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }
    }
}

