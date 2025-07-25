using Workbit.Core.Models.Manager;

namespace Workbit.Core.Interfaces
{
    public interface IManagerService
    {
        // Read
        Task<IEnumerable<ManagerSummaryDto>> GetAllAsync();
        Task<IEnumerable<ManagerSummaryDto>> GetByDepartmentIdAsync(int departmentId);
        Task<ManagerReadDto> GetByIdAsync(string id);

        // Update
        Task UpdateAsync(ManagerUpdateDto dto);


        // Helpers
        Task<bool> ExistsByIdAsync(string id);
    }
}
