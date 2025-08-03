using Workbit.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Payment;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;
using static Workbit.Common.DataConstants.Constants;
using Workbit.Core.Models.Employee;

namespace Workbit.Core.Services
{
    public class PaymentService : IPaymentService
	{
		private readonly IRepository repository;

		public PaymentService(IRepository _repository)
		{
			repository = _repository;
		}

		public Task AllocateDepartmentBudgetAsync(int departmentId,
                                                decimal totalBudget, 
                                                decimal bonusPool,
                                                DateTime period)
		{
			throw new NotImplementedException();
		}


		public async Task<IEnumerable<PaymentReadDto>> GetByEmployeeIdAsync(string employeeId)
		{
			var user = await repository.GetByIdAsync<ApplicationUser>(Guid.Parse(employeeId));

			return user.Payments
				.OrderByDescending(p => p.PaymentDate)
				.Select(p => new PaymentReadDto
				{
					Id = p.Id,
					RecipientId = p.RecipientId.ToString(),
					RecipientName = p.Recipient.FullName,
					PaymentDate = p.PaymentDate,
					Salary = p.Salary,
					Bonus = p.Bonus,
					Taxes = p.Taxes,
					Notes = p.Notes
				})
				.ToList();
		}

		private decimal CalculateTaxes(decimal salary, decimal bonus)
		{
			var total = salary + bonus;
			return total * 0.10m; // 10% tax as placeholder
		}

        public async Task<IEnumerable<PaymentReadDto>> GetAllByCeoIdAsync(
                                                                string ceoId,DateTime? startDate = null,
                                                                DateTime? endDate = null,string role = "All")
        {
            // Fetch all tracked payments (no AsNoTracking so lazy loading works)
            var payments = await repository.All<Payment>()
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();  // This ensures tracking and lazy loading

            // Apply filters in-memory (IEnumerable)
            var filtered = payments.Where(p =>
            {
                // Date filter
                if (startDate.HasValue && p.PaymentDate < startDate.Value) return false;
                if (endDate.HasValue && p.PaymentDate > endDate.Value) return false;

                // CEO filter via lazy loading
                var isManagerPayment = p.Recipient.Manager != null &&
                                       p.Recipient.Manager.Department.Company.CeoId.ToString() == ceoId;

                var isEmployeePayment = p.Recipient.Employee != null &&
                                        p.Recipient.Employee.Job.Department.Company.CeoId.ToString() == ceoId;

                if (!(isManagerPayment || isEmployeePayment)) return false;

                // Role filter
                if (role == "Manager" && p.Recipient.Manager == null) return false;
                if (role == "Employee" && p.Recipient.Employee == null) return false;

                return true;
            });

            // Project to DTO
            var result = filtered.Select(p => new PaymentReadDto
            {
                Id = p.Id,
                RecipientId = p.RecipientId.ToString(),
                RecipientName = p.Recipient.FullName,
                PaymentDate = p.PaymentDate,  // Can add formatted version here
                Salary = p.Salary,
                Bonus = p.Bonus,
                Taxes = p.Taxes,
                Notes = p.Notes
            }).ToList();

            return result;
        }

        public async Task PayManagerAsync(PayManagerViewModel model)
		{
            var payment = new Payment
            {
                RecipientId = Guid.Parse( model.ManagerId),         
                Salary = model.Salary,
                Bonus = model.Bonus,
                Taxes = model.Taxes,
                PaymentDate = DateTime.UtcNow,
                Notes = model.Notes
            };

            // Save to the database
            await repository.AddAsync(payment);
            await repository.SaveChangesAsync();

        }

            public async Task PayEmployeeByManagerAsync(PayEmployeeFormModel model)
            {
                var manager = await repository.All<Manager>()
                    .FirstOrDefaultAsync(m => m.ApplicationUserId == model.ManagerId);

                var employee = await repository.All<Employee>()
                    .FirstOrDefaultAsync(e => e.ApplicationUserId == model.EmployeeId);

			    var budget = manager.Department.DepartmentBudgets.OrderByDescending(d => d.DateAllocated).First();

                var payment = new Payment
                {
                    RecipientId = model.EmployeeId,
                    Salary = model.Salary,
                    Bonus = model.Bonus,
                    Taxes = CalculateTaxes(model.Salary, model.Bonus),
                    PaymentDate = DateTime.UtcNow,
                    Notes = model.Notes ?? string.Empty
                };

                await repository.AddAsync(payment);

                var sum = model.Salary + model.Bonus - payment.Taxes;

                if (budget.TotalBudget < sum)
                    throw new InvalidOperationException(
                        $"Insufficient budget: available {budget.TotalBudget}, required {sum-budget.TotalBudget}.");

                budget.TotalBudget -= sum;

                await repository.SaveChangesAsync();
            }

        public async Task<IEnumerable<PaymentReadDto>> GetAllByManagerIdAsync(
                                                                string managerId,
                                                                DateTime? startDate = null,
                                                                DateTime? endDate = null)
        {
            var payments = await repository.All<Payment>()
                                        .OrderByDescending(p => p.PaymentDate)
                                        .ToListAsync();

            var filtered = payments.Where(p =>
            {
                var paymentDate = p.PaymentDate.Date;

                if (startDate.HasValue && paymentDate < startDate.Value.Date) return false;
                if (endDate.HasValue && paymentDate > endDate.Value.Date) return false;

                if (p.Recipient.Employee == null) return false;

                if (p.Recipient.Employee.Job == null) return false;

                return p.Recipient.Employee.Job.Department.Managers
                    .Any(m => m.ApplicationUserId.ToString() == managerId);
            });

            return filtered.Select(p => new PaymentReadDto
            {
                Id = p.Id,
                RecipientId = p.RecipientId.ToString(),
                RecipientName = p.Recipient.FullName,
                PaymentDate = p.PaymentDate,
                Salary = p.Salary,
                Bonus = p.Bonus,
                Taxes = p.Taxes,
                Notes = p.Notes
            }).ToList();
        }
        public async Task<EmployeePaymentsViewModel> GetAllByEmployeeIdAsync(
                                                                string employeeId,
                                                                DateTime? startDate = null,
                                                                DateTime? endDate = null)
        {
            var payments = await repository.All<Payment>()
                .Where(p=> p.RecipientId.ToString() == employeeId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            var filtered = payments.Where(p =>
            {
                var paymentDate = p.PaymentDate.Date;
                if (startDate.HasValue && paymentDate < startDate.Value.Date) return false;
                if (endDate.HasValue && paymentDate > endDate.Value.Date) return false;

                return true;
            });

            var paymentsResult =  filtered.Select(p => new PaymentReadDto
            {
                Id = p.Id,
                RecipientId = p.RecipientId.ToString(),
                RecipientName = p.Recipient.FullName,
                PaymentDate = p.PaymentDate,
                Salary = p.Salary,
                Bonus = p.Bonus,
                Taxes = p.Taxes,
                Notes = p.Notes
            }).ToList();

            return new EmployeePaymentsViewModel
            {
                Payments = paymentsResult,
                StartDate = startDate?.ToString(DateFormatShort),
                EndDate = endDate?.ToString(DateFormatShort),
            };
        }

        public async Task<IEnumerable<EmployeePaymentViewModel>> GetEmployeePaymentModelByDepartmentIdAsync(int departmentId)
        {
            var department = await repository.GetByIdAsync<Department>(departmentId);

            var employees = department.Jobs
                                            .SelectMany(p => p.Employees)
                                            .Select(e=> new EmployeePaymentViewModel 
                                            {
                                                Id = e.ApplicationUserId.ToString(),
                                                FullName = e.ApplicationUser.FullName,
                                                IBAN = e.IBAN,
                                                Salary = e.EffectiveSalary
                                            
                                            }).ToList();
            return employees;
        }
    }
}
