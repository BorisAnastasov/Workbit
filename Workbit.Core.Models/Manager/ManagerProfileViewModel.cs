namespace Workbit.Core.Models.Manager
{
    public class ManagerProfileViewModel
    {
        // Manager info (can be populated from ManagerReadDto)
        public string ManagerId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        // Department context
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        // Team stats
        public int TotalTeamMembers { get; set; }       // Total employees in manager's department
        public int PresentToday { get; set; }           // Employees present today in the department

        // Job/structure stats
        public int TotalJobs { get; set; }              // Count of job roles in the department

        // Financial stats
        public decimal DepartmentPayrollThisMonth { get; set; }  // Total payroll paid to team this month
        public decimal DepartmentBudget { get; set; }            // Allocated department budget

        // Optional: quick team listing
        public List<string> TeamEmployees { get; set; } = new();
    }
}

