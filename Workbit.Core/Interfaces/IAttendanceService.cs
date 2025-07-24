using Workbit.Core.Models.Attendance;

namespace Workbit.Core.Interfaces
{
    public interface IAttendanceService
    {
        // Add a new check-in or check-out
        Task CreateAsync(AttendanceEntryCreateDto dto);

        // Read
        Task<IEnumerable<AttendanceEntryReadDto>> GetByUserIdAsync(string userId);
        Task<IEnumerable<AttendanceEntryReadDto>> GetByDateAsync(DateTime date);
        Task<AttendanceEntryReadDto> GetByIdAsync(int id);

        // Delete an entry
        Task DeleteAsync(int id);

        // Summaries
        Task<IEnumerable<DailyAttendanceSummaryDto>> GetMonthlySummaryAsync(string userId, int year, int month);
        Task<int> CountAbsencesAsync(string userId, int year, int month);
    }
}
