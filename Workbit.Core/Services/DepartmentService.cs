using LearnSpace.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Department;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository repository;

        public DepartmentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(DepartmentCreateDto dto)
        {
            var department = new Department()
            {
                Name = dto.Name,
                Description = dto.Description,
                CompanyId = dto.CompanyId
            };

            await repository.AddAsync(department);
            await repository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync<Department>(id);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            var department = await repository.GetByIdAsync<Department>(id);

            return department != null;
        }

        public async Task<IEnumerable<DepartmentSummaryDto>> GetAllAsync()
        {
            return await repository.AllReadOnly<Department>()
                                        .Select(d => new DepartmentSummaryDto
                                        {
                                            Id = d.Id,
                                            Name = d.Name
                                        })
                                        .ToListAsync();
        }

        public async Task<IEnumerable<DepartmentSummaryDto>> GetAllByCompanyIdAsync(int companyId)
        {

            return await repository.AllReadOnly<Department>()
                .Where(d => d.CompanyId == companyId).Select(d => new DepartmentSummaryDto
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToListAsync();
        }

        public async Task<DepartmentReadDto> GetByIdAsync(int id)
        {
            var department = await repository.GetByIdAsync<Department>(id);

            return new DepartmentReadDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                CompanyId = department.CompanyId,
                ManagerNames = department.Managers.Select(m => m.ApplicationUser.FullName).ToList(),
                JobTitles = department.Jobs.Select(j => j.Title).ToList()
            };
        }

        public async Task UpdateAsync(DepartmentUpdateDto dto)
        {
            var department = await repository.GetByIdAsync<Department>(dto.Id);

            department.Name = dto.Name;
            department.Description = dto.Description;

            await repository.SaveChangesAsync();

        }
    }
}
