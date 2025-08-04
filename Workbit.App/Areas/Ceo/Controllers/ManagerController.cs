using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Employee;
using Workbit.Core.Models.Manager;

namespace Workbit.App.Controllers
{
    public class ManagerController : BaseController
    {
        private readonly IManagerService managerService;
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly IJobService jobService;

        public ManagerController(IManagerService _managerService, IEmployeeService _employeeService, IJobService _jobService, IDepartmentService _departmentService)
        {
            this.managerService = _managerService;
            this.employeeService = _employeeService;
            this.jobService = _jobService;
            this.departmentService = _departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var manager = await managerService.GetByIdAsync(id); // You’ll need a ManagerReadDto for details
            return View(manager);
        }

        [HttpGet]
        public async Task<IActionResult> AssignManager(int departmentId)
        {
            // Get department info

            if (!await departmentService.ExistByIdAsync(departmentId)) 
            {
                return RedirectToAction("Error500", "Error");
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

        [HttpPost]
        public async Task<IActionResult> AssignManager(AssignManagerViewModel model)
        {
            if (string.IsNullOrEmpty(model.SelectedManagerId))
            {
                ModelState.AddModelError(nameof(model.SelectedManagerId), "Please select a manager.");
            }

            if (!ModelState.IsValid)
            {
                // Repopulate managers for the dropdown so the view works
                model.AvailableManagers = await managerService.GetUnassignedManagersAsync();
                // Keep DepartmentId so the form stays on the right department
                return View(model);  // Do NOT redirect, stay on same page
            }

            var success = await managerService.AssignToDepartmentAsync(model.SelectedManagerId!, model.DepartmentId);

            if (!success)
            {
                TempData["Error"] = "Failed to assign manager.";
                return RedirectToAction("DepartmentDetails", "Department", new { id = model.DepartmentId });
            }

            TempData["Success"] = "Manager successfully assigned.";
            return RedirectToAction("DepartmentDetails", "Department", new { id = model.DepartmentId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveManager(string managerId, int departmentId)
        {
            try
            {
                await managerService.RemoveFromDepartmentAsync(managerId);
                return RedirectToAction("DepartmentDetails", "Department", new { id = departmentId });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }
	}
}
