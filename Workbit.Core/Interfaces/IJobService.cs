using Workbit.Core.Models.Job;

namespace Workbit.Core.Interfaces
{
    public interface IJobService
    {
        //Create
        Task CreateJobAsync(JobCreateViewModel model);

        // Read
        Task<IEnumerable<JobSummaryDto>> GetAllByCompanyIdAsync(int companyId);
        Task<IEnumerable<JobSummaryDto>> GetByDepartmentIdAsync(int departmentId);
        Task<JobReadDto> GetByIdAsync(int id);
        Task<JobEditViewModel> GetJobEditModelAsync(int jobId);

        // Delete
        Task DeleteAsync(int id);

        // Helpers
        Task<bool> HasEmployeesAsync(int jobId);
        Task<bool> ExistsByIdAsync(int id);
        Task EditJobAsync(JobEditViewModel model);
    }
}
