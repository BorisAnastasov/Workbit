using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Services;

namespace Workbit.App.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;

        public AttendanceController(IAttendanceService _attendanceService)
        {
            this.attendanceService = _attendanceService;
        }

        // View all check-in/out logs with filters
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

        // Daily summary (aggregated per person/day)
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

            return View(summary); // returns List<DailyAttendanceSummaryDto>
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn()
        {
            var success = await attendanceService.CheckInAsync(User.Id());
            TempData[success ? "Success" : "Error"] =
                success ? "You have successfully checked in!" : "You already checked in today.";
            return RedirectToAction("EmployeeDashboard", "Employee");
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
            var success = await attendanceService.CheckOutAsync(User.Id());
            TempData[success ? "Success" : "Error"] =
                success ? "You have successfully checked out!" : "You must check in first or already checked out.";
            return RedirectToAction("EmployeeDashboard", "Employee");
        }
    }
}
