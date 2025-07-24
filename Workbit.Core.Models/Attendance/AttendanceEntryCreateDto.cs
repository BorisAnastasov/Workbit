namespace Workbit.Core.Models.Attendance
{
    public class AttendanceEntryCreateDto
    {
        public string UserId { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string Type { get; set; } = null!;
    }
}
