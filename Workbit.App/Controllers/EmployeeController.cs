using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Services;

namespace Workbit.App.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IAttendanceService attendanceService;

        public EmployeeController(IEmployeeService _employeeService, IAttendanceService _attendanceService)
        {
            this.employeeService = _employeeService;
            this.attendanceService = _attendanceService;
        }

        public async Task<IActionResult> AllEmployees()
        {
            var employees = await employeeService.GetAllByCeoIdAsync(User.Id());
            return View(employees);        }

        public async Task<IActionResult> EmployeeDetails(string id)
        {
            var employee = await employeeService.GetByIdAsync(id); // returns EmployeeReadDto

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDashboard()
        {
            try
            {
                if (!await employeeService.ExistsByIdAsync(User.Id())) 
                {
                    return RedirectToAction("Error404", "Error");
                }

                var employee = await employeeService.GetByIdAsync(User.Id());
                if (employee.JobId == null) 
                {
                    return RedirectToAction("Error500", "Error");
                }
                
                var dashboard = await employeeService.GetDashboardAsync(User.Id());

                var isCheckedIn = await attendanceService.IsCheckedInAsync(User.Id());

                ViewBag.IsCheckedIn = isCheckedIn;
                ViewBag.CheckInStatusMessage = isCheckedIn
                    ? "You are currently checked in today."
                    : "You haven't checked in yet today.";

                return View(dashboard);
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
                await employeeService.LeaveDepartmentAsync(User.Id());

                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }
    }
}
