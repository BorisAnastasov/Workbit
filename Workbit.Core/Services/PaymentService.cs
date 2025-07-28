using Workbit.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Payment;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Core.Services
{
    public class PaymentService : IPaymentService
	{
		private readonly IRepository repository;

		public PaymentService(IRepository _repository)
		{
			repository = _repository;
		}

		public Task AllocateDepartmentBudgetAsync(int departmentId, decimal totalBudget, decimal bonusPool, DateTime period)
		{
			throw new NotImplementedException();
		}

		public async Task CreateAsync(PaymentCreateDto dto)
		{
			var payment = new Payment
			{
				RecipientId = Guid.Parse(dto.EmployeeId),
				PaymentDate = dto.PaymentDate,
				Salary = (decimal)dto.Salary,
				Bonus = (decimal)dto.Bonus,
				Taxes = (decimal)dto.Taxes,
				Notes = dto.Notes
			};

			await repository.AddAsync(payment);
			await repository.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			await repository.DeleteAsync<Payment>(id);
			await repository.SaveChangesAsync();
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			var payment = await repository.GetByIdAsync<Payment>(id);

			return payment != null;
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

		public async Task<PaymentReadDto> GetByIdAsync(int id)
		{
			var payment = await repository.GetByIdAsync<Payment>(id);

			return new PaymentReadDto
			{
				Id = payment.Id,
				RecipientId = payment.RecipientId.ToString(),
				RecipientName = payment.Recipient.FullName,
				PaymentDate = payment.PaymentDate,
				Salary = payment.Salary,
				Bonus = payment.Bonus,
				Taxes = payment.Taxes,
				Notes = payment.Notes
			};
		}

		public async Task<decimal> GetTotalPaidToEmployeeAsync(string employeeId)
		{
			var user = await repository.GetByIdAsync<ApplicationUser>(employeeId);

			return user.Payments
				.Sum(p => p.Salary + p.Bonus - p.Taxes);
		}

		public async Task PayManagerAsync(Guid managerUserId, decimal salary, decimal bonus, string? notes)
		{
			// Find the Employee (Manager is still stored in Employee table)
			var manager = await repository.AllReadOnly<Manager>()
				.FirstOrDefaultAsync(m => m.ApplicationUserId == managerUserId);


			var payment = new Payment
			{
				RecipientId = managerUserId,
				Salary = salary,
				Bonus = bonus,
				Taxes = CalculateTaxes(salary, bonus),
				PaymentDate = DateTime.UtcNow,
				Notes = notes
			};

			await repository.AddAsync(payment);
			await repository.SaveChangesAsync();
		}

		private decimal CalculateTaxes(decimal salary, decimal bonus)
		{
			var total = salary + bonus;
			return total * 0.10m; // 10% tax as placeholder
		}

		public async Task UpdateAsync(PaymentUpdateDto dto)
		{
			var payment = await repository.GetByIdAsync<Payment>(dto.Id);

			payment.Salary = (decimal)dto.Salary;
			payment.Bonus = (decimal)dto.Bonus;
			payment.Taxes = (decimal)dto.Taxes;
			payment.Notes = dto.Notes;

			await repository.SaveChangesAsync();
		}

        public async Task<IEnumerable<PaymentReadDto>> GetAllByCeoIdAsync(
    string ceoId,
    DateTime? startDate = null,
    DateTime? endDate = null,
    string role = "All")
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

        public async Task PayEmployeeByManagerAsync(PayEmployeeDto dto)
        {
            // Load manager (lazy loading will handle Department)
            var manager = await repository.AllReadOnly<Manager>()
                .FirstOrDefaultAsync(m => m.ApplicationUserId == dto.ManagerId);

            // Load employee (lazy loading will handle Job and Department)
            var employee = await repository.AllReadOnly<Employee>()
                .FirstOrDefaultAsync(e => e.ApplicationUserId == dto.EmployeeId);

			var db = manager.Department.DepartmentBudgets.OrderByDescending(d => d.DateAllocated).First();
            // Ensure manager has enough budget
            if (db.TotalBudget < (dto.Salary + dto.Bonus))
            {
                throw new InvalidOperationException("Insufficient budget to complete this payment.");
            }

            // Create the payment record
            var payment = new Payment
            {
                RecipientId = dto.EmployeeId,
                Salary = dto.Salary,
                Bonus = dto.Bonus,
                Taxes = CalculateTaxes(dto.Salary, dto.Bonus),
                PaymentDate = DateTime.UtcNow,
                Notes = dto.Notes ?? string.Empty
            };

            await repository.AddAsync(payment);

            // Deduct from department's remaining budget
            db.TotalBudget -= (dto.Salary + dto.Bonus);
            repository.Update(manager.Department);  // Ensure EF tracks the change

            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentReadDto>> GetAllByManagerIdAsync(
    string managerId,
    DateTime? startDate = null,
    DateTime? endDate = null)
        {
            // Load all payments with their Recipient -> Employee -> Job -> Department -> Managers
            var payments = await repository.All<Payment>()
    .Include(p => p.Recipient)
        .ThenInclude(r => r.Employee)               // Force load Employee
            .ThenInclude(e => e.Job)                // Load Job
                .ThenInclude(j => j.Department)     // Load Department
                    .ThenInclude(d => d.Managers)   // Load Managers
    .OrderByDescending(p => p.PaymentDate)
    .ToListAsync();


            var filtered = payments.Where(p =>
            {
                var paymentDate = p.PaymentDate.Date;

                // Date filtering (ignore time)
                if (startDate.HasValue && paymentDate < startDate.Value.Date) return false;
                if (endDate.HasValue && paymentDate > endDate.Value.Date) return false;

                // Skip payments that are NOT for employees (e.g., manager payments)
                if (p.Recipient.Employee == null) return false;

                // Ensure the employee is in a department managed by this manager
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
        public async Task<IEnumerable<PaymentReadDto>> GetAllByEmployeeIdAsync(
    string employeeId,
    DateTime? startDate = null,
    DateTime? endDate = null)
        {
            var payments = await repository.All<Payment>()
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            var filtered = payments.Where(p =>
            {
                // Match employee payments
                if (p.RecipientId.ToString() != employeeId) return false;

                // Date filters
                var paymentDate = p.PaymentDate.Date;
                if (startDate.HasValue && paymentDate < startDate.Value.Date) return false;
                if (endDate.HasValue && paymentDate > endDate.Value.Date) return false;

                return true;
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


    }
}
