using Microsoft.AspNetCore.Identity;
using Workbit.Core.Models.Country;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Core.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult?> RegisterUserAsync(ApplicationUser user, string password);
        Task RegisterEmployeeAsync(Employee employee);    
        Task RegisterManagerAsync(Manager manager);    
        Task RegisterCeoAsync(Ceo ceo);
        Task LogoutAsync();
        Task<ApplicationUser?> FindUserByEmailAsync(string email);
        Task<bool> LoginAsync(ApplicationUser user, string password);

        Task<IEnumerable<CountrySummaryDto>> GetCountries();
        
    }
}
