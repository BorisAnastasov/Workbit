﻿using Workbit.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Department;
using Workbit.Core.Models.Job;
using Workbit.Core.Models.Manager;
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

        public async Task<IEnumerable<DepartmentSummaryDto>> GetAllByCeoIdAsync(string ceoId)
        {
            var company = await repository.AllReadOnly<Company>().FirstAsync(c => c.CeoId.ToString() == ceoId);
            return company.Departments.Select(d => new DepartmentSummaryDto
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();
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
                Managers = department.Managers.Select(m => new ManagerSummaryDto { FullName = m.ApplicationUser.FullName, Id = m.ApplicationUserId.ToString() }).ToList(),
                Jobs = department.Jobs.Select(j => new JobSummaryDto { Title = j.Title, Id = j.Id } ).ToList()
            };
        }

        public async Task UpdateAsync(DepartmentUpdateDto dto)
        {
            var department = await repository.GetByIdAsync<Department>(dto.Id);

            department.Name = dto.Name;
            department.Description = dto.Description;

            await repository.SaveChangesAsync();

        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await repository.GetByIdAsync<Department>(id);

            repository.Delete(department);
            await repository.SaveChangesAsync();
        }
    }
}
