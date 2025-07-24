using LearnSpace.Infrastructure.Database.Repository;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Payment;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Core.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IRepository repository;

        public PaymentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(PaymentCreateDto dto)
        {
            var payment = new Payment
            {
                EmployeeId = Guid.Parse(dto.EmployeeId),
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

        public async Task<IEnumerable<PaymentReadDto>?> GetByEmployeeIdAsync(string employeeId)
        {
            var employee = await repository.GetByIdAsync<Employee>(Guid.Parse(employeeId));

            return employee.Payments
                .OrderByDescending(p => p.PaymentDate)
                .Select(p => new PaymentReadDto
                {
                    Id = p.Id,
                    EmployeeId = p.EmployeeId.ToString(),
                    EmployeeName = p.Employee.ApplicationUser.FullName,
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
                EmployeeId = payment.EmployeeId.ToString(),
                EmployeeName = payment.Employee.ApplicationUser.FullName,
                PaymentDate = payment.PaymentDate,
                Salary = payment.Salary,
                Bonus = payment.Bonus,
                Taxes = payment.Taxes,
                Notes = payment.Notes
            };
        }

        public async Task<decimal> GetTotalPaidToEmployeeAsync(string employeeId)
        {
            var employee = await repository.GetByIdAsync<Employee>(employeeId);

            return employee.Payments
                .Sum(p => p.Salary + p.Bonus - p.Taxes);
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
    }
}
