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
        public int TotalTeamMembers { get; set; }
        public int PresentToday { get; set; }

        // Job/structure stats
        public int TotalJobs { get; set; }

        // Financial stats
        public double DepartmentPayrollThisMonth { get; set; }
        public double DepartmentBudget { get; set; }

        public IEnumerable<ManagerSummaryDto> Colleagues { get; set; } = new List<ManagerSummaryDto>();

    }
}

