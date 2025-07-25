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
                DepartmentId = manager.DepartmentId,
                DepartmentName = manager.Department?.Name,
                TeamEmployees = departmentEmployees
            };
        }

        public async Task UpdateAsync(ManagerUpdateDto dto)
        {
            var guid = Guid.Parse(dto.Id);
            var manager = await repository.GetByIdAsync<Manager>(guid);

            manager.DepartmentId = dto.DepartmentId;
            manager.ApplicationUser.PhoneNumber = dto.PhoneNumber;

            await repository.SaveChangesAsync();
        }
    }
}
