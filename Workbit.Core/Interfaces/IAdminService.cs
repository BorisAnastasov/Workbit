using Workbit.Core.Models.Admin;

namespace Workbit.Core.Interfaces
{
    public interface IAdminService
    {
        Task<AdminUserListViewModel> GetAllUsersForAdminAsync(
            string? search, string? role);

        Task DeleteUserAsync(string userId);

        Task<bool> UserExistsById(string userId);

        Task<UserDetailsViewModel?> GetUserDetailsAsync(string userId);

	}
}
