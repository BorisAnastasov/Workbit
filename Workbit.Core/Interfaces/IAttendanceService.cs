using Workbit.Core.Models.Attendance;

namespace Workbit.Core.Interfaces
{
    public interface IAttendanceService
    {
        // Add a new check-in or check-out
        Task CreateAsync(AttendanceEntryCreateDto dto);

        Task CheckInAsync(string userId);
        Task CheckOutAsync(string userId);
        Task<bool> IsCheckedInAsync(string userId);
        Task<bool> IsCheckedOutAsync(string userId);

        // Read
        Task<IEnumerable<AttendanceEntryViewModel>> GetByUserIdAsync(string userId);
        Task<IEnumerable<AttendanceEntryViewModel>> GetByDateAsync(DateTime date, string ceoId);
        Task<AttendanceEntryViewModel> GetByIdAsync(int id);

        // Delete an entry
        Task DeleteAsync(int id);

        // Summaries
        Task<IEnumerable<DailyAttendanceSummaryModel>> GetMonthlySummaryAsync(string userId, int year, int month);
        Task<int> CountAbsencesAsync(string userId, int year, int month);
        Task<List<AttendanceEntryViewModel>> GetAttendanceLogsAsync(DateTime start, DateTime end, string role);
        Task<List<DailyAttendanceSummaryModel>> GetDailySummaryAsync(DateTime start, DateTime end, string role);
    }
}
