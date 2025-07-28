using Workbit.Core.Models.Job;
using Workbit.Core.Models.Manager;

namespace Workbit.Core.Models.Department
{
    public class DepartmentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CompanyId { get; set; }

        public List<ManagerSummaryDto> Managers{ get; set; } = new List<ManagerSummaryDto>();
        public List<JobSummaryDto> Jobs { get; set; } = new List<JobSummaryDto>();
    }
}
