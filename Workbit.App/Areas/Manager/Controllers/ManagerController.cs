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
			try
			{
                if (!await managerService.ExistsByIdAsync(User.Id())) 
                {
					return RedirectToAction("Error404", "Error");
				}

                if (!await managerService.HasDepartmentByUserIdAsync(User.Id())) 
                {
                    return RedirectToAction(nameof(NoDepartment), "Manager", new { area = "Manager" });
                }

				var profile = await managerService.GetProfileDataAsync(User.Id());

				return View(profile);
			}
			catch (Exception)
			{
				return RedirectToAction("Error500", "Error");
			}
		}

        [HttpGet]
        public IActionResult NoDepartment()
        {
            return View();
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

                if (!await managerService.HasDepartmentByUserIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoDepartment), "Manager", new { area = "Manager" });
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

        [HttpPost]
        public async Task<IActionResult> KickEmployee(string id)
        {
            try
            {
                await managerService.RemoveEmployeeByIdAsync(id);
                TempData["Success"] = "Employee removed from your department.";

				return RedirectToAction(nameof(TeamEmployees));

			}
			catch (Exception)
            {
				return RedirectToAction("Error500", "Error");
			}

        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDetailsForManager(string id)
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error");
                }

                if (!await employeeService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error");
                }

                var employeeDetails = await employeeService.GetByIdAsync(id);

                return View(employeeDetails);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentJobs()
        {
            try
            {
                var manager = await managerService.GetByIdAsync(User.Id());
                if (manager.DepartmentId == null)
                {
                    return RedirectToAction("Error403", "Error");
                }

                var jobs = await jobService.GetByDepartmentIdAsync(manager.DepartmentId.Value);
                return View("DepartmentJobs", jobs);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
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

			var model = new HireEmployeeViewModel
			{
				AvailableUsers = await employeeService.GetUnemployedUsersAsync(),
				AvailableJobs = await employeeService.GetJobsForManagerAsync(managerId)
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Hire(HireEmployeeViewModel model)
		{
			await employeeService.HireEmployeeAsync(model.SelectedUserId, model.SelectedJobId, model.Level);

			TempData["Success"] = "Employee hired successfully!";
			return RedirectToAction("Hire");
		}
	}
}
