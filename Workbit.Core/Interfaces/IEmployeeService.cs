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
        Task<EmployeeReadDto> GetByIdAsync(string userId);
        Task<EmployeeProfileViewModel> GetProfileAsync(string employeeId, WorkingDaysApi response, int? month = null);
        Task<IEnumerable<EmployeeSummaryDto>> GetAllByCeoIdAsync(string userId);
        Task<string> GetCountryCodeByEmployeeIdAsync(string userId);

        // Update
        Task UpdateAsync(EmployeeUpdateDto dto);

        // Delete
        Task DeleteAsync(string userId);

        // Helpers
        Task<bool> ExistsByIdAsync(string userId);
        Task<bool> HasPaymentsAsync(string userId);
        Task<bool> HasJobAsync(string userId);
		Task<List<EmployeeSummaryDto>> GetUnemployedUsersAsync();
		Task<List<JobSummaryDto>> GetJobsForManagerAsync(string managerId);
		Task HireEmployeeAsync(string userId, int jobId, string level);
        Task LeaveJobAsync(string userId);
        Task FireEmployeeByIdAsync(string userId);

    }
}
