using LearnSpace.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Employee;
using Workbit.Core.Models.Payment;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Core.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IRepository repository;

        public EmployeeService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task DeleteAsync(string id)
        {
            var guid = Guid.Parse(id);

            await repository.DeleteAsync<Employee>(guid);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(id));

            return employee != null;
        }

        public async Task<IEnumerable<EmployeeSummaryDto>> GetAllAsync()
        {
            var employees = await repository.AllReadOnly<Employee>()
                .Select(e => new EmployeeSummaryDto
                {
                    Id = e.ApplicationUserId.ToString(),
                    FullName = e.ApplicationUser.FullName
                })
                .ToListAsync();

            return employees;
        }

        public async Task<IEnumerable<EmployeeSummaryDto>> GetByDepartmentIdAsync(int departmentId)
        {
            var employees = await repository.AllReadOnly<Employee>()
                .Where(e => e.Job.DepartmentId == departmentId)
                .Select(e => new EmployeeSummaryDto 
                {
                    Id = e.ApplicationUserId.ToString(),
                    FullName = e.ApplicationUser.FullName
                })
                .ToListAsync();

            return employees;
        }

        public async Task<EmployeeReadDto> GetByIdAsync(string id)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(id));

            return new EmployeeReadDto
            {
                Id = employee.ApplicationUserId.ToString(),
                FullName = employee.ApplicationUser.FullName,
                Email = employee.ApplicationUser.Email!,
                JobId = employee.JobId,
                JobTitle = employee.Job.Title,
                DepartmentId = employee.Job.DepartmentId,
                DepartmentName = employee.Job.Department.Name,
                Level = employee.Level.ToString(),
                Payments = employee.Payments.Select(p => new PaymentSummaryDto
                {
                    PaymentDate = p.PaymentDate,
                    Salary = p.Salary,
                    Bonus = p.Bonus,
                    Taxes = p.Taxes
                }).ToList()
            };
        }

        public async Task<IEnumerable<EmployeeSummaryDto>> GetByJobIdAsync(int jobId)
        {
            var employees = await repository.AllReadOnly<Employee>()
                .Where(e => e.JobId == jobId)
                .Select(e => new EmployeeSummaryDto
                {
                    Id = e.ApplicationUserId.ToString(),
                    FullName = e.ApplicationUser.FullName
                })
                .ToListAsync();

            return employees;
        }

        public async Task<bool> HasPaymentsAsync(string id)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(id));

            return employee.Payments.Any();
        }

        public async Task UpdateAsync(EmployeeUpdateDto dto)
        {
            var guid = Guid.Parse(dto.Id);
            var employee = await repository.GetByIdAsync<Employee>(guid);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {dto.Id} not found.");
            }

            employee.JobId = dto.JobId;
            employee.Level = Enum.Parse<Workbit.Infrastructure.Enumerations.JobLevel>(dto.Level, true);

            await repository.SaveChangesAsync();
        }
    }
}
