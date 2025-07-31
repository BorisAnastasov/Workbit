using Workbit.Core.Models.ApiNinjas;

namespace Workbit.Core.Models.Employee
{
    public class EmployeeProfileViewModel
    {
        // Basic info
        public string EmployeeId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public string Level { get; set; } = null!;

        // Attendance summary for current month
        public int TotalPresentDays { get; set; }
        public int TotalAbsentDays { get; set; }
        public double HoursWorkedThisMonth { get; set; }

        // Payroll summary for current month
        public decimal TotalPaidThisMonth { get; set; }

        // New: API Ninjas Working Days data
        public WorkingDaysApi WorkingDaysResponse { get; set; }
        public int SelectedMonth { get; set; }
        public string Country { get; set; } = null!;
        public int WorkingDays => WorkingDaysResponse?.WorkingDays?.Count ?? 0;
        public int WorkingDaysElapsed { get; set; } // Set in your service: working days <= today
        public double AttendancePercentage
            => WorkingDaysElapsed > 0 ? (double)TotalPresentDays / WorkingDaysElapsed * 100 : 0;
        public double AbsencePercentage
            => WorkingDaysElapsed > 0 ? (double)TotalAbsentDays / WorkingDaysElapsed * 100 : 0;
    }
}
