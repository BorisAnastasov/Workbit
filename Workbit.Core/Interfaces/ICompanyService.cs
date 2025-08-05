using Workbit.Core.Models.Company;

namespace Workbit.Core.Interfaces
{
    public interface ICompanyService
    {
        // Create
        Task CreateAsync(CompanyFormModel dto);

        // Read
        Task<IEnumerable<CompanySummaryModel>> GetAllAsync();
        Task<CompanyViewModel> GetByIdAsync(int id);

        // Delete
        Task DeleteAsync(int id);

        // Helpers
        Task<bool> ExistsByIdAsync(int id);
        Task<string?> GetCeoIdAsync(int companyId);
        Task<CompanyViewModel> GetByCeoIdAsync(string ceoId);
    }
}
