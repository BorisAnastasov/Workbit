using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Employee;
using Workbit.Core.Models.Manager;

namespace Workbit.App.Areas.Manager.Controllers
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
		public async Task<IActionResult> Profile()
		{
			//try
			{
                if (!await managerService.ExistsByIdAsync(User.Id())) 
                {
					return RedirectToAction("Error404", "Error", new { area = "" });
                }

                if (!await managerService.HasDepartmentByManagerIdAsync(User.Id())) 
                {
                    return RedirectToAction(nameof(NoDepartment), "Base", new { area = "Manager" });
                }

				var profile = await managerService.GetProfileDataAsync(User.Id());

				return View(profile);
			}
			//catch (Exception)
			{
				return RedirectToAction("Error500", "Error", new { area="" });
			}
		}

        [HttpGet]
        public async Task<IActionResult> TeamEmployees() 
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error");
                }

                if (!await managerService.HasDepartmentByManagerIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoDepartment), "Base", new { area = "Manager" });
                }
                var manager = await managerService.GetByIdAsync(User.Id());

                var employees = await employeeService.GetByDepartmentIdAsync(manager.DepartmentId!.Value);

				return View(employees);
			}
			catch (Exception)
			{
				return RedirectToAction("Error500", "Error");
			}
		}

        [HttpPost]
        public async Task<IActionResult> LeaveDepartment()
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error");
                }

                await managerService.LeaveDepartmentAsync(User.Id());

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

		[HttpGet]
		public async Task<IActionResult> ColleagueDetails(string userId)
		{
            try
            {
                var model = await managerService.GetByIdAsync(userId);

                return View(model);
            }
            catch (Exception)
            {
				return RedirectToAction("Error500", "Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Hire()
		{
			var managerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;

			var model = new EmployeeHireViewModel
			{
				AvailableUsers = await employeeService.GetUnemployedUsersAsync(),
				AvailableJobs = await employeeService.GetJobsForManagerAsync(managerId)
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Hire(EmployeeHireViewModel model)
		{
			await employeeService.HireEmployeeAsync(model.SelectedUserId, model.SelectedJobId, model.Level);

			TempData["Success"] = "Employee hired successfully!";

			return RedirectToAction(nameof(Hire));
		}
	}
}
