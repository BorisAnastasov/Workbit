using LearnSpace.Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Ceo;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;
using static Workbit.Common.RoleConstants;

namespace Workbit.Core.Services
{
    public class CeoService : ICeoService
    {
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public CeoService(IRepository _repository, UserManager<ApplicationUser> _userManager)
        {
            repository = _repository;
            userManager = _userManager;
        }

        public async Task AssignCeoAsync(CeoCreateDto dto)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(dto.UserId);

            var newCeo = new Ceo
            {
                ApplicationUserId = Guid.Parse(dto.UserId),
            };

            await repository.AddAsync(newCeo);

            // Assign Identity role
            if (user != null && !await userManager.IsInRoleAsync(user, CeoRoleName))
            {
                await userManager.AddToRoleAsync(user, CeoRoleName);
            }

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string userId)
        {
            var ceo = await repository.GetByIdAsync<Ceo>(Guid.Parse(userId));

            return ceo != null;
        }

        public async Task<IEnumerable<CeoReadDto>> GetAllAsync()
        {
            return await repository.AllReadOnly<Ceo>()
                .Select(c => new CeoReadDto
                {
                    UserId = c.ApplicationUserId.ToString(),
                    FullName = c.ApplicationUser.FullName,
                    Email = c.ApplicationUser.Email!,
                    PhoneNumber = c.ApplicationUser.PhoneNumber,
                })
                .ToListAsync();
        }

        public async Task<CeoReadDto> GetByCompanyIdAsync(int companyId)
        {
            var company = await repository.GetByIdAsync<Company>(companyId);
            var ceo = company.Ceo;

            var ceoReaddto = new CeoReadDto
            {
                UserId = ceo.ApplicationUserId.ToString(),
                FullName = ceo.ApplicationUser.FullName,
                Email = ceo.ApplicationUser.Email!,
                PhoneNumber = ceo.ApplicationUser.PhoneNumber,
                CompanyId = company.Id,
                CompanyName = company.Name
            };

            return ceoReaddto;
        }

        public async Task<CeoReadDto> GetByUserIdAsync(string userId)
        {
            var guid = Guid.Parse(userId);
            var ceo = await repository.GetByIdAsync<Ceo>(guid);
            var company = await repository.AllReadOnly<Company>().FirstOrDefaultAsync(c=>c.CeoId == guid);

            var ceoReaddto = new CeoReadDto
            {
                UserId = ceo.ApplicationUserId.ToString(),
                FullName = ceo.ApplicationUser.FullName,
                Email = ceo.ApplicationUser.Email!,
                PhoneNumber = ceo.ApplicationUser.PhoneNumber,
                CompanyId = company?.Id,
                CompanyName = company?.Name
            };

            return ceoReaddto;
        }

        public async Task RemoveCeoAsync(string userId)
        {
            var guid = Guid.Parse(userId);

            var user = await userManager.FindByIdAsync(userId);
            if (user != null && await userManager.IsInRoleAsync(user, CeoRoleName))
            {
                await userManager.RemoveFromRoleAsync(user, CeoRoleName);
            }

            await repository.DeleteAsync<Ceo>(guid);
            await repository.SaveChangesAsync();
        }
    }
}
