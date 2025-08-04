using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Interfaces;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService attendanceService;

        public AttendanceController(IAttendanceService _attendanceService)
        {
            attendanceService = _attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> AttendanceLog(DateTime? startDate,
                                                        DateTime? endDate,
                                                        string role = "All")
        {
            startDate ??= DateTime.Today;
            endDate ??= DateTime.Today;

            var logs = await attendanceService.GetAttendanceLogsAsync(
                startDate.Value, endDate.Value, role
            );

            ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");
            ViewBag.Role = role;

            return View(logs);
        }
    }
}
