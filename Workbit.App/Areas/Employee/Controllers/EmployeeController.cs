using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;

namespace Workbit.App.Areas.Employee.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService employeeService;
        private readonly IAttendanceService attendanceService;
        private readonly IApiNinjasService apiNinjasService;

        public EmployeeController(IEmployeeService _employeeService,
                                IAttendanceService _attendanceService,
                                IApiNinjasService _apiNinjasService)
        {
            employeeService = _employeeService;
            attendanceService = _attendanceService;
            apiNinjasService = _apiNinjasService;
        }

        public async Task<IActionResult> AllEmployees()
        {
            var employees = await employeeService.GetAllByCeoIdAsync(User.Id());
            return View(employees);
        }

        public async Task<IActionResult> EmployeeDetails(string id)
        {
            var employee = await employeeService.GetByIdAsync(id); // returns EmployeeReadDto

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int? month)
        {
            try
            {
                var userId = User.Id();

                if (!await employeeService.ExistsByIdAsync(userId))
                {
                    return RedirectToAction("Error404", "Error");
                }

                if (!await employeeService.HasJobAsync(userId))
                {
                    return RedirectToAction("Error500", "Error");
                }

                int selectedMonth = month ?? DateTime.UtcNow.Month;

                var profile = await employeeService.GetProfileAsync(userId, selectedMonth);

                // Get working days from API Ninjas for the employee’s country
                var workingDaysResponse = await apiNinjasService.GetWorkingDaysAsync(profile.Country, selectedMonth);
                profile.WorkingDays = workingDaysResponse;

                var isCheckedIn = await attendanceService.IsCheckedInAsync(userId);
                ViewBag.IsCheckedIn = isCheckedIn;
                ViewBag.CheckInStatusMessage = isCheckedIn
                    ? "You are currently checked in today."
                    : "You haven't checked in yet today.";

                return View(profile);
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
