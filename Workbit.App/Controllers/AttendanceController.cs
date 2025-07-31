using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;

namespace Workbit.App.Controllers
{
	public class AttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;

        public AttendanceController(IAttendanceService _attendanceService)
        {
            this.attendanceService = _attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> AttendanceLog(DateTime? startDate, DateTime? endDate, string role = "All")
        {
            startDate ??= DateTime.Today;
            endDate ??= DateTime.Today;

            var logs = await attendanceService.GetAttendanceLogsAsync(
                startDate.Value, endDate.Value, role
            );

            ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");
            ViewBag.Role = role;

            return View(logs); // returns List<AttendanceEntryReadDto>
        }

        [HttpGet]
        public async Task<IActionResult> DailySummary(DateTime? startDate, DateTime? endDate, string role = "All")
        {
            startDate ??= DateTime.Today.AddDays(-7); // Default last 7 days
            endDate ??= DateTime.Today;

            var summary = await attendanceService.GetDailySummaryAsync(
                startDate.Value, endDate.Value, role
            );

            ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");
            ViewBag.Role = role;

            return View(summary);
        }
    }
}
