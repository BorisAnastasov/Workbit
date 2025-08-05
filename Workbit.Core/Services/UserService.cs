using Microsoft.AspNetCore.Identity;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Country;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Database.Repository;
using static Workbit.Common.RoleConstants;

namespace Workbit.Core.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository repository;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        private readonly ICountryService countryService;


        public UserService(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<IdentityRole<Guid>> _roleManager,
            ICountryService _countryService,
            IRepository _repository)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            countryService = _countryService;
            repository = _repository;
        }

        public async Task<ApplicationUser?> FindUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<IEnumerable<CountrySummaryModel>> GetCountries()
        {
            return await countryService.GetCountrySummariesAsync();
        }

        public async Task<bool> LoginAsync(ApplicationUser user, string password)
        {
            var result = await signInManager.PasswordSignInAsync(user, password, false, false);

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task RegisterCeoAsync(Ceo ceo)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(ceo.ApplicationUserId);

            await userManager.AddToRoleAsync(user, CeoRoleName);

            await repository.AddAsync(ceo);
            await repository.SaveChangesAsync();
        }

        public async Task RegisterEmployeeAsync(Employee employee)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(employee.ApplicationUserId);

            await userManager.AddToRoleAsync(user, EmployeeRoleName);

            await repository.AddAsync(employee);
            await repository.SaveChangesAsync();
        }

        public async Task RegisterManagerAsync(Manager manager)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(manager.ApplicationUserId);

            await userManager.AddToRoleAsync(user, ManagerRoleName);

            await repository.AddAsync(manager);
            await repository.SaveChangesAsync();
        }

        public async Task<IdentityResult?> RegisterUserAsync(ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);

            return result;
        }
    }
}
