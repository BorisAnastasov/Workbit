using Workbit.Infrastructure.Database.Repository;
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

            var ceo = await repository.GetByIdAsync<Ceo>(Guid.Parse(dto.CeoId));


            await repository.AddAsync(company);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = await repository.All<Company>()
                            .Include(c => c.Departments)
                            .ThenInclude(d => d.Managers)
                            .Include(c => c.Departments)
                            .ThenInclude(d => d.Jobs)
                            .ThenInclude(j => j.Employees)
                            .FirstOrDefaultAsync(c => c.Id == id);

            foreach (var manager in company.Departments.SelectMany(d => d.Managers))
            {
                manager.DepartmentId = null;
            }

            // Unassign all Employees (set JobId to null)
            foreach (var employee in company.Departments.SelectMany(d => d.Jobs.SelectMany(j => j.Employees)))
            {
                employee.JobId = null;
            }

            var userIds = company.Departments
                    .SelectMany(d => d.Managers.Select(m => m.ApplicationUserId))
                    .Concat(company.Departments.SelectMany(d => d.Jobs.SelectMany(j => j.Employees.Select(e => e.ApplicationUserId))))
                    .Distinct()
                    .ToList();

            var attendanceEntries = repository.All<AttendanceEntry>()
                                .Where(a => userIds.Contains(a.UserId))
                                .ToList();
            repository.DeleteRange(attendanceEntries);

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
                CeoName = company.Ceo.ApplicationUser.FullName,
                Departments = company.Departments.Select(d => d.Name).ToList()
            };
        }

        public async Task<string?> GetCeoIdAsync(int companyId)
        {
            var company = await repository.GetByIdAsync<Company>(companyId);
            return company?.CeoId.ToString();
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
