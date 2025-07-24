using Workbit.Core.Models.Company;

namespace Workbit.Core.Interfaces
{
    public interface ICompanyService
    {
        // Create
        Task CreateAsync(CompanyCreateDto dto);

        // Read
        Task<IEnumerable<CompanySummaryDto>> GetAllAsync();
        Task<CompanyReadDto> GetByIdAsync(int id);

        // Update
        Task UpdateAsync(CompanyUpdateDto dto);

        // Delete
        Task DeleteAsync(int id);

        // Helpers
        Task<bool> ExistsByIdAsync(int id);
        Task<string?> GetCeoIdAsync(int companyId);
        Task MakeCeoAsync(int companyId, string newCeoUserId, string currentCeoId);
    }
}
