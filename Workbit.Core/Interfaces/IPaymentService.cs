using Workbit.Core.Models.Payment;

namespace Workbit.Core.Interfaces
{
    public interface IPaymentService
    {
        // Create
        Task CreateAsync(PaymentCreateDto dto);

        // Read
        Task<IEnumerable<PaymentReadDto>?> GetByEmployeeIdAsync(string employeeId);
        Task<PaymentReadDto> GetByIdAsync(int id);

        // Update
        Task UpdateAsync(PaymentUpdateDto dto);

        // Delete
        Task DeleteAsync(int id);

        // Helpers
        Task<bool> ExistsByIdAsync(int id);
        Task<decimal> GetTotalPaidToEmployeeAsync(string employeeId);
    }
}
