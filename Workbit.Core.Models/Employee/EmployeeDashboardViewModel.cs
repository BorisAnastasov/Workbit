using Workbit.Core.Models.Payment;

namespace Workbit.Core.Models.Employee
{
    public class EmployeeDashboardViewModel
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

        // Recent payments (e.g. last 5)
        public List<PaymentSummaryDto> RecentPayments { get; set; } = new();
    }
}
