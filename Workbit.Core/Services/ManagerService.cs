using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Manager;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Database.Repository;
using Workbit.Infrastructure.Enumerations;

namespace Workbit.Core.Services
{
    public class ManagerService : IManagerService
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

        public async Task<IEnumerable<ManagerSummaryDto>> GetAllByCeoIdAsync(string id)
        {
            return await repository.AllReadOnly<Manager>()
                                .Where(m => m.Department.Company.CeoId.ToString() == id)
                                .Select(m => new ManagerSummaryDto
                                {
                                    Id = m.ApplicationUserId.ToString(),
                                    FullName = m.ApplicationUser.FullName,
                                }).ToListAsync();
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


		public async Task<ManagerProfileViewModel> GetProfileDataAsync(string managerId)
		{
			var guid = Guid.Parse(managerId);

			// Load manager with related data (department, jobs, user)
			var manager = await repository.AllReadOnly<Manager>()
				.FirstAsync(m => m.ApplicationUserId == guid);

			var department = manager.Department;

            // Employees in the manager's department
            var totalTeamMembers = await repository.AllReadOnly<Employee>()
                .Where(e => e.Job.DepartmentId == department.Id).CountAsync();


			// Employees present today (unique check-ins)
			var presentToday = await repository.AllReadOnly<AttendanceEntry>()
				.Where(a => a.User.Employee != null &&
							a.User.Employee.Job.DepartmentId == department.Id &&
							a.Type == EntryType.CheckIn &&
							a.Timestamp.Date == DateTime.UtcNow.Date)
				.Select(a => a.UserId)
				.Distinct()
				.CountAsync();

			// Total payroll for this department this month
			var payrollThisMonth = await repository.AllReadOnly<Payment>()
				.Where(p => p.Recipient.Employee != null &&
							p.Recipient.Employee.Job.DepartmentId == department.Id &&
							p.PaymentDate.Month == DateTime.UtcNow.Month &&
							p.PaymentDate.Year == DateTime.UtcNow.Year)
				.SumAsync(p => (decimal?)p.Salary + p.Bonus - p.Taxes) ?? 0;

			// Latest department budget (if any)
			var budget = await repository.AllReadOnly<DepartmentBudget>()
				.Where(b => b.DepartmentId == department.Id)
				.OrderByDescending(b => b.DateAllocated)
				.Select(b => b.TotalBudget)
				.FirstOrDefaultAsync();

			// Count distinct job roles in this department
			var totalJobs = await repository.AllReadOnly<Job>()
				.Where(j => j.DepartmentId == department.Id)
				.CountAsync();

            var colleagues = await repository.AllReadOnly<Manager>()
                                            .Where(m => m.DepartmentId == manager.DepartmentId && m.ApplicationUserId != manager.ApplicationUserId)
                                            .Select(m => new ManagerSummaryDto 
                                            {
                                                Id = m.ApplicationUserId.ToString(),
                                                FullName = m.ApplicationUser.FullName
                                            })
                                            .ToListAsync();

			return new ManagerProfileViewModel
			{
				ManagerId = manager.ApplicationUserId.ToString(),
				FullName = manager.ApplicationUser.FullName,
				Email = manager.ApplicationUser.Email!,
				DepartmentId = department.Id,
				DepartmentName = department.Name,
				TotalTeamMembers = totalTeamMembers,
				PresentToday = presentToday,
				TotalJobs = totalJobs,
				DepartmentPayrollThisMonth = (double)payrollThisMonth,
				DepartmentBudget = (double)budget,
                Colleagues = colleagues
            };
		}


		public async Task RemoveEmployeeByIdAsync(string id)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(id));
            employee.Job = null;
            employee.JobId = null;

            repository.Update(employee);
            await repository.SaveChangesAsync();
        }

        public async Task<List<ManagerSummaryDto>> GetUnassignedManagersAsync()
        {
            return await repository.AllReadOnly<Manager>()
                .Where(m => m.DepartmentId == null)
                .Select(m => new ManagerSummaryDto
                {
                    Id = m.ApplicationUserId.ToString(),
                    FullName = m.ApplicationUser.FullName
                })
                .ToListAsync();
        }

        public async Task<bool> AssignToDepartmentAsync(string managerId, int departmentId)
        {
            var manager = await repository.All<Manager>()
                .FirstOrDefaultAsync(m => m.ApplicationUserId.ToString() == managerId);

            if (manager == null)
                return false;

            var departmentExists = await repository.All<Department>()
                .AnyAsync(d => d.Id == departmentId);

            if (!departmentExists)
                return false;

            manager.DepartmentId = departmentId;
            await repository.SaveChangesAsync();
            return true;
        }

        public async Task RemoveFromDepartmentAsync(string managerId)
        {
            var manager = await repository.GetByIdAsync<Manager>(Guid.Parse(managerId));

            manager.DepartmentId = null;

            await repository.SaveChangesAsync();
        }

        public async Task LeaveDepartmentAsync(string managerId)
        {
            var manager = await repository.All<Manager>()
                .FirstOrDefaultAsync(m => m.ApplicationUserId.ToString() == managerId);

            if (manager == null || manager.DepartmentId == null)
                return;

            manager.DepartmentId = null;

            await repository.SaveChangesAsync();
        }

        public async Task<bool> HasDepartmentByUserIdAsync(string userId)
        {
            var manager = await repository.GetByIdAsync<Manager>(Guid.Parse(userId));

            return manager.DepartmentId != null;
        }
    }
}
