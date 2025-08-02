using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Employee;

namespace Workbit.App.Areas.Manager.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService employeeService;
        private readonly IManagerService managerService;

        public EmployeeController(IEmployeeService _employeeService, IManagerService _managerService)
        {
            this.employeeService = _employeeService;
            this.managerService = _managerService;
        }

        [HttpGet]
        public async Task<IActionResult> Team()
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error");
                }

                if (!await managerService.HasDepartmentByManagerIdAsync(User.Id()))
                {
                    return RedirectToAction("NoDepartment", "Manager", new { area = "Manager" });
                }

                var departmentId = await managerService.GetDepartmentIdByManagerIdAsync(User.Id());

                var employees = await employeeService.GetByDepartmentIdAsync(departmentId);

                return View(employees);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Fire(string employeeId)
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                if (!await managerService.HasDepartmentByManagerIdAsync(User.Id()))
                {
                    return RedirectToAction("NoDepartment", "Manager", new { area = "Manager" });
                }

                var departmentId = await managerService.GetDepartmentIdByManagerIdAsync(User.Id());

                if (!await managerService.HasJobFromDepartmentAsync(departmentId, employeeId))
                {
                    return RedirectToAction("Error404", "Error", new {area = ""});
                }

                await employeeService.FireEmployeeByIdAsync(employeeId);

                TempData["Success"] = "Employee removed from your department.";

                return RedirectToAction(nameof(Team));

            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }

        }
        [HttpGet]
        public async Task<IActionResult> Details(string employeeId)
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("NoDepartment", "Manager", new { area = "Manager" });
                }

                if (!await employeeService.ExistsByIdAsync(employeeId))
                {
                    return RedirectToAction("Error404", "Error");
                }

                if (!await managerService.IsManagerOfEmployeeAsync(User.Id(), employeeId)) 
                {
                    return RedirectToAction("Error403", "Error");
                }
                var employeeDetails = await employeeService.GetByIdAsync(employeeId);

                return View(employeeDetails);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Hire()
        {
            var model = new EmployeeHireViewModel
            {
                AvailableUsers = await employeeService.GetUnemployedUsersAsync(),
                AvailableJobs = await employeeService.GetJobsForManagerAsync(User.Id())
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Hire(EmployeeHireViewModel model)
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                var departmentId = await managerService.GetDepartmentIdByManagerIdAsync(User.Id());

                if (await managerService.HasJobFromDepartmentAsync(departmentId, model.SelectedUserId))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                await employeeService.HireEmployeeAsync(model.SelectedUserId, model.SelectedJobId, model.Level);

                TempData["Success"] = "Employee hired successfully!";

                return RedirectToAction(nameof(Team));
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
            
        }

    }
}
