using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Department;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService departmentService;
        private readonly ICeoService ceoService;

        public DepartmentController(IDepartmentService _departmentService, ICeoService _ceoService)
        {
            departmentService = _departmentService;
            ceoService = _ceoService;
        }

        public async Task<IActionResult> All()
        {
            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }
                var departments = await departmentService.GetAllByCeoIdAsync(User.Id());
                return View(departments);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }
                var department = await departmentService.GetByIdAsync(id);

                return View(department);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }

                var ceo = await ceoService.GetByUserIdAsync(User.Id());

                var model = new DepartmentCreateFormModel
                {
                    CompanyId = ceo.CompanyId!.Value
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateFormModel model)
        {
            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await departmentService.CreateAsync(model);

                return RedirectToAction(nameof(All), new {area="Ceo"});
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }
                await departmentService.DeleteDepartmentAsync(id);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }
    }
}

