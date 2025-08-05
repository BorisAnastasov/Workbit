namespace Workbit.Core.Models.Attendance
{
    public class DailyAttendanceSummaryModel
    {
        public DateTime Date { get; set; }
        public TimeSpan? FirstCheckIn { get; set; }
        public TimeSpan? LastCheckOut { get; set; }
        public double HoursWorked { get; set; } // Calculated
        public string Status { get; set; } = null!; // "Present", "Absent", "Late"
    }
}
