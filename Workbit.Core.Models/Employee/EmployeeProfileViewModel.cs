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
        public int SelectedMonth { get; set; }  // Which month is selected (1–12)
        public int WorkingDays { get; set; }    // API result for that month
        public string Country { get; set; } = null!;  // Employee’s country (ISO code)
    }
}
