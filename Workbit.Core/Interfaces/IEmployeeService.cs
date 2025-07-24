using Workbit.Core.Models.Employee;

namespace Workbit.Core.Interfaces
{
    public interface IEmployeeService
    {
        //Create
        Task CreateAsync(EmployeeCreateDto dto);

        // Read
        Task<IEnumerable<EmployeeSummaryDto>> GetAllAsync();
        Task<IEnumerable<EmployeeSummaryDto>> GetByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<EmployeeSummaryDto>> GetByJobIdAsync(int jobId);
        Task<EmployeeReadDto> GetByIdAsync(string id);

        // Update
        Task UpdateAsync(EmployeeUpdateDto dto);

        // Delete
        Task DeleteAsync(string id);

        // Helpers
        Task<bool> ExistsByIdAsync(string id);
        Task<bool> HasPaymentsAsync(string id);
    }
}
