using Workbit.Core.Models.Job;

namespace Workbit.Core.Models.Employee
{
	public class EmployeeHireViewModel
	{
		public string SelectedUserId { get; set; } = string.Empty;
		public int SelectedJobId { get; set; }
		public string Level { get; set; } = "Junior";

		public List<EmployeeSummaryModel> AvailableUsers { get; set; } = new();
		public List<JobSummaryDto> AvailableJobs { get; set; } = new();
	}
}
