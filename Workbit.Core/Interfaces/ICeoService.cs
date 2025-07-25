using Workbit.Core.Models.Ceo;

namespace Workbit.Core.Interfaces
{
    public interface ICeoService
    {
        //Create
        Task AssignCeoAsync(CeoCreateDto dto);

        // Remove a CEO
        Task RemoveCeoAsync(string userId);

        // Get CEO info
        Task<CeoReadDto> GetByCompanyIdAsync(int companyId);
        Task<CeoReadDto> GetByUserIdAsync(string userId);
        Task<IEnumerable<CeoReadDto>> GetAllAsync();

        // New helper
        Task<bool> ExistsByIdAsync(string userId);
    }
}
