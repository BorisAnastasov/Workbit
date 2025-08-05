using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Manager;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class ManagerController : BaseController
    {
        private readonly IManagerService managerService;
        private readonly IDepartmentService departmentService;

        public ManagerController(IManagerService _managerService, IDepartmentService _departmentService)
        {
            this.managerService = _managerService;
            this.departmentService = _departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var manager = await managerService.GetByIdAsync(id);
                return View(manager);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignManager(int departmentId)
        {
            try
            {
                if (!await departmentService.ExistByIdAsync(departmentId))
                {
                    return RedirectToAction("Error403", "Error", new { area = "" });
                }

                var managers = await managerService.GetUnassignedManagersAsync();
                var department = await departmentService.GetByIdAsync(departmentId);

                var viewModel = new AssignManagerViewModel
                {
                    DepartmentId = department.Id,
                    DepartmentName = department.Name,
                    AvailableManagers = managers
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignManager(AssignManagerViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.SelectedManagerId))
                {
                    ModelState.AddModelError(nameof(model.SelectedManagerId), "Please select a manager.");
                }

                if (!ModelState.IsValid)
                {
                    model.AvailableManagers = await managerService.GetUnassignedManagersAsync();
                    return View(model);  
                }

                var success = await managerService.AssignToDepartmentAsync(model.SelectedManagerId!, model.DepartmentId);

                if (!success)
                {
                    TempData["Error"] = "Failed to assign manager.";
                    return RedirectToAction(nameof(AssignManager), new { departmentId = model.DepartmentId });
                }

                TempData["Success"] = "Manager successfully assigned.";
                return RedirectToAction(nameof(AssignManager), new { departmentId = model.DepartmentId });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveManager(string managerId, int departmentId)
        {
            try
            {
                await managerService.RemoveFromDepartmentAsync(managerId);
                return RedirectToAction("Details", "Department", new { id = departmentId });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new {area=""});
            }
        }
	}
}
