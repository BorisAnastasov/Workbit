using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Admin;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Database.Repository;

namespace Workbit.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminService(IRepository repository, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<AdminUserListViewModel> GetAllUsersForAdminAsync(
                                                string? search, string? role = "All")
        {
            var users = await repository.AllReadOnly<ApplicationUser>().ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string normalizedSearch = search.ToLower();

                users = users.Where(u =>
                        u.FirstName.Contains(normalizedSearch) ||
                        u.LastName.Contains(normalizedSearch) ||
                        u.Email!.Contains(normalizedSearch)).ToList();
            }


            var userDtos = new List<AdminUserDto>();



            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault() ?? "Unknown";

                if (role != "All" &&
                !string.Equals(userRole, role, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                userDtos.Add(new AdminUserDto
                {
                    Id = user.Id.ToString(),
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email!,
                    Role = userRole,
                    IsEmployed = user.Employee?.Job != null || user.Manager?.Department != null || user.Ceo != null
                });
            }

            return new AdminUserListViewModel
            {
                SearchTerm = search,
                SelectedRole = role,
                Users = userDtos
            };
        }
        public async Task DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            await userManager.DeleteAsync(user);
            await repository.SaveChangesAsync();
        }

		public async Task<bool> UserExistsById(string userId)
		{
            var user = await repository.GetByIdAsync<ApplicationUser>(Guid.Parse(userId));

            return user != null;
		}

		public async Task<UserDetailsViewModel?> GetUserDetailsAsync(string userId)
		{
			var user = await repository.AllReadOnly<ApplicationUser>()
				.Where(u => u.Id.ToString() == userId)
				.FirstOrDefaultAsync();

			var roles = await userManager.GetRolesAsync(user);
			var role = roles.FirstOrDefault() ?? "Unknown";

			return new UserDetailsViewModel
			{
				Id = user.Id.ToString(),
				FullName = $"{user.FirstName} {user.LastName}",
				Email = user.Email ?? "N/A",
				PhoneNumber = user.PhoneNumber ?? "N/A",
				DateOfBirth = user.DateOfBirth,
				Role = role,
			};
		}

	}
}
