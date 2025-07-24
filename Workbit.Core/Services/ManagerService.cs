using LearnSpace.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Manager;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Core.Services
{
    public class ManagerService:IManagerService
    {
        private readonly IRepository repository;

        public ManagerService(IRepository _repository)
        {
            repository = _repository;

        }

        public async Task CreateAsync(ManagerCreateDto dto)
        {
            var manager = new Manager
            {
                ApplicationUserId = Guid.Parse(dto.ApplicationUserId),
                DepartmentId = dto.DepartmentId,
                OfficePhone = dto.OfficePhone,
                IsCeo = dto.IsCeo
            };

            await repository.AddAsync(manager);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await repository.DeleteAsync<Manager>(Guid.Parse(id));
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            var manager = await repository.GetByIdAsync<Manager>(Guid.Parse(id));

            return manager != null;
        }

        public async Task<IEnumerable<ManagerSummaryDto>> GetAllAsync()
        {
            var managers = await repository.AllReadOnly<Manager>()
                .Select(m => new ManagerSummaryDto
                {
                    Id = m.ApplicationUserId.ToString(),
                    FullName = m.ApplicationUser.FullName,
                    IsCeo = m.IsCeo
                })
                .ToListAsync();

            return managers;
        }

        public async Task<IEnumerable<ManagerSummaryDto>> GetByDepartmentIdAsync(int departmentId)
        {
            var managers = await repository.AllReadOnly<Manager>()
                .Where(m => m.DepartmentId == departmentId)
                .Select(m => new ManagerSummaryDto
                {
                    Id = m.ApplicationUserId.ToString(),
                    FullName = m.ApplicationUser.FullName,
                    IsCeo = m.IsCeo
                })
                .ToListAsync();

            return managers;
        }

        public async Task<ManagerReadDto> GetByIdAsync(string id)
        {
            var manager = await repository.GetByIdAsync<Manager>(Guid.Parse(id));

            var departmentEmployees = manager.Department.Jobs
               .SelectMany(j => j.Employees)
               .Select(e => e.ApplicationUser.FullName)
               .Distinct()
               .ToList();

            return new ManagerReadDto
            {
                Id = manager.ApplicationUserId.ToString(),
                FullName = manager.ApplicationUser.FullName,
                Email = manager.ApplicationUser.Email!,
                OfficePhone = manager.OfficePhone,
                IsCeo = manager.IsCeo,
                DepartmentId = manager.DepartmentId,
                DepartmentName = manager.Department.Name,
                TeamEmployees = departmentEmployees
            };
        }

        public async Task<ManagerReadDto> GetCeoAsync()
        {
            var ceo = await repository.All<Manager>()
                .FirstOrDefaultAsync(m => m.IsCeo);

            var teamEmployees = ceo.Department.Jobs
            .SelectMany(j => j.Employees)
            .Select(e => e.ApplicationUser.FullName)
            .Distinct()
            .ToList();

            return new ManagerReadDto
            {
                Id = ceo.ApplicationUserId.ToString(),
                FullName = ceo.ApplicationUser.FullName,
                Email = ceo.ApplicationUser.Email,
                OfficePhone = ceo.OfficePhone,
                IsCeo = ceo.IsCeo,
                DepartmentId = ceo.DepartmentId,
                DepartmentName = ceo.Department.Name,
                TeamEmployees = teamEmployees
            };
        }

        public async Task UpdateAsync(ManagerUpdateDto dto)
        {
            var guid = Guid.Parse(dto.Id);
            var manager = await repository.GetByIdAsync<Manager>(guid);

            manager.DepartmentId = dto.DepartmentId;
            manager.OfficePhone = dto.OfficePhone;
            manager.IsCeo = dto.IsCeo;

            await repository.SaveChangesAsync();
        }
    }
}
