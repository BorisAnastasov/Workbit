using Workbit.Core.Models.Employee;
using Workbit.Core.Models.Payment;

namespace Workbit.Core.Interfaces
{
    public interface IPaymentService
    {
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

        Task<EmployeePaymentsViewModel> GetAllByEmployeeIdAsync(
                                                        string employeeId,
                                                        DateTime? startDate = null,
                                                        DateTime? endDate = null);

        Task<IEnumerable<EmployeePaymentViewModel>> GetEmployeePaymentModelByDepartmentIdAsync(int departmentId);
        Task PayManagerAsync(PayManagerViewModel model);
        Task PayEmployeeByManagerAsync(PayEmployeeFormModel model);
    }
}
