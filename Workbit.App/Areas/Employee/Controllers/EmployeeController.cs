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
            this.employeeService = _employeeService;
			this.attendanceService = _attendanceService;
			this.apiNinjasService = _apiNinjasService;
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
                    return RedirectToAction(nameof(NoJob), "Employee", new { area = "Employee"});
                }

                int selectedMonth = month ?? DateTime.UtcNow.Month;

                var countryCode = await employeeService.GetCountryCodeByIdAsync(userId);

                var workingDaysResponse = await apiNinjasService.GetWorkingDaysAsync(countryCode, selectedMonth);

                var profile = await employeeService.GetProfileAsync(userId, workingDaysResponse, selectedMonth);

                var isCheckedIn = await attendanceService.IsCheckedInAsync(userId);
                var isCheckedOut = await attendanceService.IsCheckedOutAsync(userId);

				ViewBag.HasChekedInToday = isCheckedIn;
                ViewBag.HasCheckedOutToday = isCheckedOut;
                ViewBag.IsTodayWorkingDay = apiNinjasService.IsTodayWorkingDayAsync(workingDaysResponse);

				return View(profile);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LeaveJob()
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
					return RedirectToAction(nameof(NoJob), "Employee", new { area = "Employee" });
				}

				await employeeService.LeaveJobAsync(User.Id());

                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }
        [HttpGet]
        public IActionResult NoJob() 
        {
            return View();
        }
	}
}
