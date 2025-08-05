using Workbit.Core.Models.Manager;

namespace Workbit.Core.Interfaces
{
    public interface IManagerService
    {
        // Read
        Task<IEnumerable<ManagerSummaryDto>> GetAllbyCeoIdAsync(string ceoId);
        Task<IEnumerable<ManagerSummaryDto>> GetByDepartmentIdAsync(int departmentId);
        Task<ManagerReadDto> GetByIdAsync(string id);
        Task<ManagerProfileViewModel> GetProfileDataAsync(string managerId);
		Task<IEnumerable<ManagerSummaryDto>> GetAllByCeoIdAsync(string id);


        // Helpers
        Task<bool> ExistsByIdAsync(string id);
        Task<List<ManagerSummaryDto>> GetUnassignedManagersAsync();
        Task<bool> AssignToDepartmentAsync(string managerId, int departmentId);
        Task LeaveDepartmentAsync(string managerId);
        Task RemoveFromDepartmentAsync(string managerId);
        Task<bool> HasDepartmentByManagerIdAsync(string userId);
        Task<bool> HasJobFromDepartmentAsync(int departmentId, string userId);
        Task<int> GetDepartmentIdByManagerIdAsync(string userId);
        Task<bool> IsManagerOfEmployeeAsync(string managerId ,string employeeId);
        Task<bool> IsManagerOfJobAsync(string managerId, int jobId);


    }
}
