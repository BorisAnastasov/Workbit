using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;

namespace Workbit.App.Areas.Employee.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService attendanceService;

        public AttendanceController(IAttendanceService _attendanceService)
        {
            this.attendanceService = _attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn()
        {
            try
            {
				var userId = User.Id();
				if (await attendanceService.IsCheckedInAsync(userId))
				{
					return RedirectToAction("Profile", "Employee", new { area = "Employee" });
				}

				await attendanceService.CheckInAsync(User.Id());


				return RedirectToAction("Profile", "Employee", new { area = "Employee" });
			}
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
			try
			{
				var userId = User.Id();

				if (await attendanceService.IsCheckedOutAsync(userId))
				{
					return RedirectToAction("Profile", "Employee", new { area = "Employee" });
				}

				await attendanceService.CheckOutAsync(User.Id());

				return RedirectToAction("Profile", "Employee", new { area = "Employee" });
			}
			catch (Exception)
			{
				return RedirectToAction("Error500", "Error", new { area = "" }); ;
			}
		}
    }
}
