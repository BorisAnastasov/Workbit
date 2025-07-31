using Workbit.Core.Models.ApiNinjas;
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
        Task<EmployeeProfileViewModel> GetProfileAsync(string employeeId,WorkingDaysApi response, int? month = null);
        Task<IEnumerable<EmployeeSummaryDto>> GetAllByCeoIdAsync(string id);
        Task<string> GetCountryCodeByIdAsync(string id);

        // Update
        Task UpdateAsync(EmployeeUpdateDto dto);

        // Delete
        Task DeleteAsync(string id);

        // Helpers
        Task<bool> ExistsByIdAsync(string id);
        Task<bool> HasPaymentsAsync(string id);
        Task<bool> HasJobAsync(string id);
		Task<List<EmployeeSummaryDto>> GetUnemployedUsersAsync();
		Task<List<JobSummaryDto>> GetJobsForManagerAsync(string managerId);
		Task HireEmployeeAsync(string userId, int jobId, string level);
        Task LeaveJobAsync(string userId);
    }
}
