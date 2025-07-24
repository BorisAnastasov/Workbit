using Workbit.Core.Models.Manager;

namespace Workbit.Core.Interfaces
{
    public interface IManagerService
    {
        // Create
        Task CreateAsync(ManagerCreateDto dto);

        // Read
        Task<IEnumerable<ManagerSummaryDto>> GetAllAsync();
        Task<IEnumerable<ManagerSummaryDto>> GetByDepartmentIdAsync(int departmentId);
        Task<ManagerReadDto> GetByIdAsync(string id);

        // Update
        Task UpdateAsync(ManagerUpdateDto dto);

        // Delete
        Task DeleteAsync(string id);

        // Helpers
        Task<bool> ExistsByIdAsync(string id);
        Task<ManagerReadDto> GetCeoAsync();  // Fetch the CEO (if any)
    }
}
