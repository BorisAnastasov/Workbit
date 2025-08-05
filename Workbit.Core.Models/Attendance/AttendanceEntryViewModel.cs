namespace Workbit.Core.Models.Attendance
{
    public class AttendanceEntryViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Timestamp { get; set; } = null!;
        public string Type { get; set; } = null!; // "CheckIn" or "CheckOut"
    }
}
