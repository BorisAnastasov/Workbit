using Workbit.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Job;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Core.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository repository;

        public JobService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateJobAsync(JobCreateViewModel model)
        {
            var job = new Job
            {
                Title = model.Title,
                Description = model.Description,
                DepartmentId = model.DepartmentId,
                BaseSalary = model.BaseSalary
            };

            await repository.AddAsync(job);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employees = await repository.All<Employee>()
                                        .Where(e => e.JobId == id).ToListAsync();

            foreach (var employee in employees) 
            {
                employee.JobId = null;
            }


            await repository.DeleteAsync<Job>(id);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            var job = await repository.GetByIdAsync<Job>(id);

            return job != null;
        }

        public async Task<IEnumerable<JobSummaryDto>> GetAllByCompanyIdAsync(int companyId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.Department.CompanyId == companyId)
                .Select(j => new JobSummaryDto
                {
                    Id = j.Id,
                    Title = j.Title
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<JobSummaryDto>> GetByDepartmentIdAsync(int departmentId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.DepartmentId == departmentId)
                .Select(j => new JobSummaryDto
                {
                    Id = j.Id,
                    Title = j.Title
                })
                .ToListAsync();
        }

        public async Task<JobReadDto> GetByIdAsync(int id)
        {
            var job = await repository.GetByIdAsync<Job>(id);

            return new JobReadDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                DepartmentId = job.DepartmentId,
                BaseSalary = (double)job.BaseSalary,
                DepartmentName = job.Department.Name,
                EmployeeNames = job.Employees.Select(e => e.ApplicationUser.FullName).ToList()
            };
        }

        public async Task<bool> HasEmployeesAsync(int jobId)
        {
            var job = await repository.GetByIdAsync<Job>(jobId);

            return job.Employees.Any();
        }

        public async Task<JobEditViewModel> GetJobEditModelAsync(int jobId)
        {
            var job = await repository.GetByIdAsync<Job>(jobId);

            var model = new JobEditViewModel
            {
                Title = job.Title,
                Description = job.Description,
                BaseSalary = job.BaseSalary,
            };

            return model;
        }

        public async Task EditJobAsync(JobEditViewModel model)
        {
            var job = await repository.GetByIdAsync<Job>(model.Id);

            job.Title = model.Title;
            job.Description = model.Description;
            job.BaseSalary = model.BaseSalary;

            await repository.SaveChangesAsync();
        }
    }
}
