﻿using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.ApiNinjas;
using Workbit.Core.Models.Employee;
using Workbit.Core.Models.Job;
using Workbit.Core.Models.Payment;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Database.Repository;
using Workbit.Infrastructure.Enumerations;

namespace Workbit.Core.Services
{
    public class EmployeeService : IEmployeeService
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

        public async Task<string> GetCountryCodeByEmployeeIdAsync(string id)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(id));

            return employee.CountryCode!;
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

        public async Task<EmployeeProfileViewModel> GetProfileAsync(
                                                    string employeeId,
                                                    WorkingDaysApi response,
                                                    int? month = null)
        {
            var guid = Guid.Parse(employeeId);
            var user = await repository.GetByIdAsync<ApplicationUser>(guid);

            var employee = user.Employee;
            var job = employee.Job;
            var department = job?.Department;

            int selectedMonth = month ?? DateTime.UtcNow.Month;
            int currentYear = DateTime.UtcNow.Year;

            // 1. Collect all attendance entries for the selected month/year
            var attendanceEntries = user.AttendanceEntries
                .Where(a => a.Timestamp.Month == selectedMonth && a.Timestamp.Year == currentYear)
                .ToList();

            // 2. List of actual working days for the month from the API (as DateTime)
            var allWorkingDays = response.WorkingDays;
            var today = DateTime.UtcNow.Date;
            // Only count working days up to today
            var workingDaysSoFar = allWorkingDays.Where(d => d <= today).ToList();

            // 3. Find unique dates the user checked in on a working day up to today
            var presentDates = attendanceEntries
                .Where(a => a.Type == EntryType.CheckIn && workingDaysSoFar.Contains(a.Timestamp.Date))
                .Select(a => a.Timestamp.Date)
                .Distinct()
                .ToList();

            // 4. Absent = working days up to today with no check-in
            var absentDates = workingDaysSoFar.Except(presentDates).ToList();

            int totalPresentDays = presentDates.Count;
            int totalAbsentDays = absentDates.Count;

            // 5. Calculate total hours worked this month (your own logic)
            double hoursWorkedThisMonth = CalculateTotalHoursWorked(attendanceEntries);

            // 6. Payroll for current month
            var payments = user.Payments
                .Where(p => p.PaymentDate.Month == selectedMonth && p.PaymentDate.Year == currentYear)
                .OrderByDescending(p => p.PaymentDate)
                .ToList();

            decimal totalPaidThisMonth = payments.Sum(p => p.Salary + p.Bonus - p.Taxes);

            // 7. Assemble the view model
            return new EmployeeProfileViewModel
            {
                EmployeeId = user.Id.ToString(),
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                DepartmentName = department?.Name ?? "--",
                JobTitle = job?.Title ?? "--",
                Level = employee.Level.ToString(),
                TotalPresentDays = totalPresentDays,
                TotalAbsentDays = totalAbsentDays,
                HoursWorkedThisMonth = hoursWorkedThisMonth,
                TotalPaidThisMonth = totalPaidThisMonth,
                SelectedMonth = selectedMonth,
                WorkingDaysResponse = response,
                Country = employee.Country.Name,
                WorkingDaysElapsed = workingDaysSoFar.Count
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
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(userId));

			employee.ApplicationUserId = Guid.Parse(userId);
            employee.JobId = jobId;
            employee.Level = Enum.Parse<JobLevel>(level, true);

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

        public async Task LeaveJobAsync(string userId)
        {
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

        public async Task FireEmployeeByIdAsync(string userId)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(userId));

            employee.JobId = null;
            employee.Level = JobLevel.Unemployed;

            await repository.SaveChangesAsync();
        }
    }
}
