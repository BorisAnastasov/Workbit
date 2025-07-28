using Workbit.Core.Models.Employee;
using Workbit.Core.Models.Job;

namespace Workbit.Core.Interfaces
{
    public interface IEmployeeService
    {
        // Read
        Task<IEnumerable<EmployeeSummaryDto>> GetAllAsync();
        Task<IEnumerable<EmployeeSummaryDto>> GetByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<EmployeeSummaryDto>> GetByJobIdAsync(int jobId);
        Task<EmployeeReadDto> GetByIdAsync(string id);
        Task<EmployeeDashboardViewModel> GetDashboardAsync(string employeeId);

        Task<IEnumerable<EmployeeSummaryDto>> GetAllByCeoIdAsync(string id);

        // Update
        Task UpdateAsync(EmployeeUpdateDto dto);

        // Delete
        Task DeleteAsync(string id);

        // Helpers
        Task<bool> ExistsByIdAsync(string id);
        Task<bool> HasPaymentsAsync(string id);

		Task<List<EmployeeSummaryDto>> GetUnemployedUsersAsync();
		Task<List<JobSummaryDto>> GetJobsForManagerAsync(string managerId);
		Task HireEmployeeAsync(string userId, int jobId, string level);
        Task LeaveDepartmentAsync(string userId);
    }
}
