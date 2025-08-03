using Workbit.Core.Models.Job;

namespace Workbit.Core.Models.Employee
{
    public class EmployeeEditViewModel
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int JobId { get; set; }
        public string Level { get; set; } = null!;

        public IEnumerable<JobSummaryDto> Jobs { get; set; } = new List<JobSummaryDto>();   
    }
}
