using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Admin;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Database.Repository;

namespace Workbit.Core.Services
{
    public class AdminService:IAdminService
    {
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

		public async Task<AdminUserListViewModel> GetAllUsersForAdminAsync(
	string? search, string? role, string? sortBy, bool sortDesc)
		{
			var query = repository.AllReadOnly<ApplicationUser>();

			// SQL filtering for performance (only search fields)
			if (!string.IsNullOrWhiteSpace(search))
			{
				query = query.Where(u =>
					u.FirstName.Contains(search) ||
					u.LastName.Contains(search) ||
					u.Email.Contains(search));
			}

			// Load users from DB (we'll handle roles & sorting in memory)
			var usersList = await query.ToListAsync();

			var userDtos = new List<AdminUserDto>();

			foreach (var user in usersList)
			{
				var roles = await userManager.GetRolesAsync(user);
				var userRole = roles.FirstOrDefault() ?? "User"; // Default to "User" if no role

				// Filter by role (AFTER fetching roles)
				if (!string.IsNullOrEmpty(role) && role != "All" &&
					!string.Equals(userRole, role, StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}

				userDtos.Add(new AdminUserDto
				{
					Id = user.Id.ToString(),
					FullName = $"{user.FirstName} {user.LastName}",
					Email = user.Email,
					Role = userRole,
					Department = user.Employee?.Job?.Department?.Name,
					IsEmployed = user.Employee != null
				});
			}

			// In-memory sorting
			userDtos = sortBy switch
			{
				"Email" => sortDesc
					? userDtos.OrderByDescending(u => u.Email).ToList()
					: userDtos.OrderBy(u => u.Email).ToList(),

				_ => sortDesc
					? userDtos.OrderByDescending(u => u.FullName).ToList()
					: userDtos.OrderBy(u => u.FullName).ToList()
			};

			return new AdminUserListViewModel
			{
				SearchTerm = search,
				SelectedRole = role,
				SortBy = sortBy,
				SortDesc = sortDesc,
				Users = userDtos
			};
		}

		public async Task DeleteUserAsync(string userId)
		{
			var user = await userManager.FindByIdAsync(userId);

			if (user.Employee != null)
				repository.Delete(user.Employee);

			if (user.Manager != null)
				repository.Delete(user.Manager);

			if (user.Ceo != null)
				repository.Delete(user.Ceo);

			await repository.SaveChangesAsync();

			await userManager.DeleteAsync(user);
		}


	}
}
