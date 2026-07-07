using Microsoft.AspNetCore.Identity;
using Workbit.Domain.Entities.Account;

namespace Workbit.Application.Interfaces
{
    public interface ICustomPasswordHasherService : IPasswordHasher<ApplicationUser>
    {
    }
}
