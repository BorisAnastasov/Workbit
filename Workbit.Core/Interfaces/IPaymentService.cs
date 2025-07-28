using Workbit.Core.Models.Payment;

namespace Workbit.Core.Interfaces
{
    public interface IPaymentService
    {
        // Create
        Task CreateAsync(PaymentCreateDto dto);

        // Read
        Task<IEnumerable<PaymentReadDto>> GetByEmployeeIdAsync(string employeeId);
        Task<IEnumerable<PaymentReadDto>> GetAllByCeoIdAsync(
                                                        string ceoId,
                                                        DateTime? startDate = null,
                                                        DateTime? endDate = null,
                                                        string role = "All");
        Task<IEnumerable<PaymentReadDto>> GetAllByManagerIdAsync(
                                                        string managerId,
                                                        DateTime? startDate = null,
                                                        DateTime? endDate = null);

        Task<IEnumerable<PaymentReadDto>> GetAllByEmployeeIdAsync(
                                                        string employeeId,
                                                        DateTime? startDate = null,
                                                        DateTime? endDate = null);


        Task<PaymentReadDto> GetByIdAsync(int id);

        // Update
        Task UpdateAsync(PaymentUpdateDto dto);

        // Delete
        Task DeleteAsync(int id);

        // Helpers
        Task<bool> ExistsByIdAsync(int id);
        Task<decimal> GetTotalPaidToEmployeeAsync(string employeeId);
        Task PayManagerAsync(PayManagerViewModel model);
        Task AllocateDepartmentBudgetAsync(int departmentId, decimal totalBudget, decimal bonusPool, DateTime period);
        Task PayEmployeeByManagerAsync(PayEmployeeDto dto);
    }
}
