using Workbit.Core.Models.Admin;

namespace Workbit.Core.Interfaces
{
    public interface IAdminService
    {
        Task<AdminUserListViewModel> GetAllUsersForAdminAsync(
            string? search, string? role, string? sortBy, bool sortDesc);

        Task DeleteUserAsync(string userId);

	}
}
