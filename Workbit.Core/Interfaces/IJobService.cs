using Workbit.Core.Models.Job;

namespace Workbit.Core.Interfaces
{
    public interface IJobService
    {
        Task CreateAsync(JobCreateDto dto);

        // Read
        Task<IEnumerable<JobSummaryDto>> GetAllByCompanyIdAsync(int companyId);
        Task<IEnumerable<JobSummaryDto>> GetByDepartmentIdAsync(int departmentId);
        Task<JobReadDto> GetByIdAsync(int id);

        // Update
        Task UpdateAsync(JobUpdateDto dto);

        // Delete
        Task DeleteAsync(int id);

        // Helpers
        Task<bool> HasEmployeesAsync(int jobId);
        Task<bool> ExistsByIdAsync(int id);
    }
}
