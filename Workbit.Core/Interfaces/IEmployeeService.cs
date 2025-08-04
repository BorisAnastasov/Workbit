using Workbit.Core.Models.ApiNinjas;
using Workbit.Core.Models.Employee;
using Workbit.Core.Models.Job;

namespace Workbit.Core.Interfaces
{
    public interface IEmployeeService
    {
        // Read
        Task<IEnumerable<EmployeeSummaryModel>> GetAllByCeoIdAsync(string ceoId);
        Task<IEnumerable<EmployeeSummaryModel>> GetByDepartmentIdAsync(int departmentId);
        Task<EmployeeViewModel> GetByIdAsync(string userId);
        Task<EmployeeProfileViewModel> GetProfileAsync(string employeeId, WorkingDaysApi response, int? month = null);
        Task<string> GetCountryCodeByEmployeeIdAsync(string userId);
        Task<EmployeeEditViewModel> GetEditModelByIdAsync(string userId);

        // Update
        Task EditEmployeeAsync(EmployeeEditViewModel dto);

        // Helpers
        Task<bool> ExistsByIdAsync(string userId);
        Task<bool> HasJobAsync(string userId);
		Task<List<EmployeeSummaryModel>> GetUnemployedUsersAsync();
		Task<List<JobSummaryDto>> GetJobsForManagerAsync(string managerId);
		Task HireEmployeeAsync(string userId, int jobId, string level);
        Task LeaveJobAsync(string userId);
        Task FireEmployeeByIdAsync(string userId);

    }
}
