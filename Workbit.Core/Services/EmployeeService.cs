using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Employee;
using Workbit.Core.Models.Job;
using Workbit.Core.Models.Payment;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Database.Repository;
using Workbit.Infrastructure.Enumerations;

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

        public async Task<IEnumerable<EmployeeSummaryDto>> GetAllByCeoIdAsync(string id)
        {
            return await repository.AllReadOnly<Employee>()
                                    .Where(e => e.Job.Department.Company.CeoId.ToString() == id)
                                    .Select(e => new EmployeeSummaryDto
                                    {
                                        Id = e.ApplicationUserId.ToString(),
                                        FullName = e.ApplicationUser.FullName,
                                    }).ToListAsync();
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
                Payments = employee.ApplicationUser.Payments.Select(p => new PaymentSummaryDto
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

        public async Task<EmployeeProfileViewModel> GetProfileAsync(string employeeId, int? month = null)
        {
            var guid = Guid.Parse(employeeId);
            var user = await repository.GetByIdAsync<ApplicationUser>(guid);

            var employee = user.Employee;
            var job = employee.Job;
            var department = job?.Department;

            int selectedMonth = month ?? DateTime.UtcNow.Month;
            int currentYear = DateTime.UtcNow.Year;

            var attendanceEntries = user.AttendanceEntries
                .Where(a => a.Timestamp.Month == selectedMonth && a.Timestamp.Year == currentYear)
                .ToList();

            int totalPresentDays = attendanceEntries
                .Where(a => a.Type == EntryType.CheckIn)
                .Select(a => a.Timestamp.Date)
                .Distinct()
                .Count();

            int totalDaysInMonth = DateTime.DaysInMonth(currentYear, selectedMonth);
            int totalAbsentDays = totalDaysInMonth - totalPresentDays;

            double hoursWorkedThisMonth = CalculateTotalHoursWorked(attendanceEntries);

            var payments = user.Payments
                .Where(p => p.PaymentDate.Month == selectedMonth && p.PaymentDate.Year == currentYear)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();

            decimal totalPaidThisMonth = payments.Sum(p => p.Salary + p.Bonus - p.Taxes);

            var recentPayments = payments
                .Take(5)
                .Select(p => new PaymentSummaryDto
                {
                    PaymentDate = p.PaymentDate,
                    Salary = p.Salary,
                    Bonus = p.Bonus,
                    Taxes = p.Taxes,
                }).ToList();

            return new EmployeeProfileViewModel
            {
                EmployeeId = user.Id.ToString(),
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                DepartmentName = department?.Name ?? "Unassigned",
                JobTitle = job?.Title ?? "Unassigned",
                Level = employee.Level.ToString(),
                TotalPresentDays = totalPresentDays,
                TotalAbsentDays = totalAbsentDays,
                HoursWorkedThisMonth = hoursWorkedThisMonth,
                TotalPaidThisMonth = totalPaidThisMonth,
                SelectedMonth = selectedMonth,
                WorkingDays = 0,
                Country = user.Country.Name
            };
        }

        private double CalculateTotalHoursWorked(IEnumerable<AttendanceEntry> entries)
        {
            var grouped = entries
                .GroupBy(e => e.Timestamp.Date)
                .ToList();

            double totalHours = 0;

            foreach (var group in grouped)
            {
                var checkIn = group.FirstOrDefault(e => e.Type == EntryType.CheckIn)?.Timestamp;
                var checkOut = group.FirstOrDefault(e => e.Type == EntryType.CheckOut)?.Timestamp;

                if (checkIn.HasValue && checkOut.HasValue)
                {
                    totalHours += (checkOut.Value - checkIn.Value).TotalHours;
                }
            }

            return Math.Round(totalHours, 2);
        }

        public async Task<bool> HasPaymentsAsync(string id)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(id));

            return employee.ApplicationUser.Payments.Any();
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

		public async Task HireEmployeeAsync(string userId, int jobId, string level)
		{
			var employee = new Employee
			{
				ApplicationUserId = Guid.Parse(userId),
				JobId = jobId,
				Level = Enum.Parse<JobLevel>(level, true)
			};

			await repository.AddAsync(employee);
			await repository.SaveChangesAsync();
		}

		public async Task<List<EmployeeSummaryDto>> GetUnemployedUsersAsync()
		{
			return await repository.AllReadOnly<Employee>()
				.Where(e => e.Job == null)
				.Select(e => new EmployeeSummaryDto
				{
					Id = e.ApplicationUserId.ToString(),
					FullName = e.ApplicationUser.FullName
				})
				.ToListAsync();
		}

		public async Task<List<JobSummaryDto>> GetJobsForManagerAsync(string managerId)
		{
			var manager = await repository.AllReadOnly<Manager>()
				.Include(m => m.Department)
				.FirstAsync(m => m.ApplicationUserId.ToString() == managerId);

			var departmentId = manager.DepartmentId ?? 0;

			return await repository.AllReadOnly<Job>()
				.Where(j => j.DepartmentId == departmentId)
				.Select(j => new JobSummaryDto
				{
					Id = j.Id,
					Title = j.Title
				})
				.ToListAsync();
		}

        public async Task LeaveDepartmentAsync(string userId)
        {
            // Find the employee record
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(userId));
            employee.Job = null;
            employee.JobId = null;
            employee.Level = JobLevel.Unemployed;
            employee.ApplicationUser.AttendanceEntries.Clear();


            repository.Update(employee);

            await repository.SaveChangesAsync();
        }

        public async Task<bool> HasJobAsync(string id)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(id));

            return employee.JobId != null;
        }
    }
}
