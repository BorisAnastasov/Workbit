using Workbit.Core.Models.Department;

namespace Workbit.Core.Interfaces
{
    public interface IDepartmentService
    {
        // Create
        Task CreateAsync(DepartmentCreateFormModel dto);

        // Read
        Task<IEnumerable<DepartmentSummaryDto>> GetAllByCeoIdAsync(string ceoId);
        Task<DepartmentReadDto> GetByIdAsync(int id);

        // Update
        Task UpdateAsync(DepartmentUpdateDto dto);

        // Delete

        Task DeleteDepartmentAsync(int id);

        //Helpers
        Task<bool> ExistByIdAsync(int id);
    }
}
