using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Services;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService attendanceService;
        private readonly ICeoService ceoService;

        public AttendanceController(IAttendanceService _attendanceService, ICeoService _ceoService)
        {
            attendanceService = _attendanceService;
            ceoService = _ceoService;
        }

        [HttpGet]
        public async Task<IActionResult> AttendanceLog(DateTime? startDate,
                                                        DateTime? endDate,
        string role = "All")
        {

            if (!await ceoService.HasCompanyByIdAsync(User.Id()))
            {
                return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
            }

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
