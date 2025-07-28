using Workbit.Core.Models.Department;
using Workbit.Core.Models.Manager;

namespace Workbit.Core.Models.Payment
{
    public class PaymentDashboardViewModel
    {
        public string CeoId { get; set; }

        public IEnumerable<ManagerSummaryDto> Managers { get; set; } = new List<ManagerSummaryDto>();

        public IEnumerable<DepartmentSummaryDto> Departments { get; set; } = new List<DepartmentSummaryDto>();

        public bool PayedManager { get; set; } = false;
        public bool AllocatedDepartmentBudget { get; set; } = false;

    }
}
