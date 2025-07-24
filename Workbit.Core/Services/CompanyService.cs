using LearnSpace.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Company;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Core.Services
{
    public class CompanyService: ICompanyService
    {
        private readonly IRepository repository;

        public CompanyService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(CompanyCreateDto dto)
        {
            var company = new Company
            {
                Name = dto.Name,
                Address = dto.Address,
                ContactPhone = dto.ContactPhone,
                CeoId = Guid.Parse(dto.CeoId)
            };

            var ceo = await repository.GetByIdAsync<Manager>(Guid.Parse(dto.CeoId));

            ceo.IsCeo = true;

            await repository.AddAsync(company);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync<Company>(id);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            var company = await repository.GetByIdAsync<Company>(id);

            return company != null;
        }

        public async Task<IEnumerable<CompanySummaryDto>> GetAllAsync()
        {
            return await repository.AllReadOnly<Company>()
                .Select(c => new CompanySummaryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CompanyReadDto> GetByIdAsync(int id)
        {
            var company = await repository.GetByIdAsync<Company>(id);

            return new CompanyReadDto
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                ContactPhone = company.ContactPhone,
                CeoId = company.CeoId.ToString(),
                CeoName = company.Ceo.FullName,
                Departments = company.Departments.Select(d => d.Name).ToList()
            };
        }

        public async Task<string?> GetCeoIdAsync(int companyId)
        {
            var company = await repository.GetByIdAsync<Company>(companyId);
            return company?.CeoId.ToString();
        }

        public async Task MakeCeoAsync(int companyId, string newCeoUserId, string currentCeoId)
        {
            var company = await repository.GetByIdAsync<Company>(companyId);

            var newCeoGuid = Guid.Parse(newCeoUserId);

            var currentCeo = await repository.GetByIdAsync<Manager>(currentCeoId);

            var newCeo = await repository.GetByIdAsync<Manager>(newCeoUserId);

            newCeo.IsCeo = true;
            currentCeo.IsCeo = false;

            company.CeoId = newCeoGuid;

            await repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(CompanyUpdateDto dto)
        {
            var company = await repository.GetByIdAsync<Company>(dto.Id);
            

            company.Name = dto.Name;
            company.Address = dto.Address;
            company.ContactPhone = dto.ContactPhone;
            company.CeoId = Guid.Parse(dto.CeoId);

            await repository.SaveChangesAsync();
        }
    }
}
