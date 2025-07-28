using Workbit.Core.Models.Employee;

namespace Workbit.Core.Models.Payment
{
    public class ManagerPaymentDashboardViewModel
    {
        public string ManagerId { get; set; }             
        public int DepartmentId { get; set; }            
        public List<EmployeeSummaryDto> Employees { get; set; } = new();

        public decimal TotalBudget { get; set; } 
    }
}
