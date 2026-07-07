using Workbit.Domain.Interfaces.Repositories;

namespace Workbit.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUserRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IManagerRepository ManagerRepository { get; }
        ICeoRepository CeoRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IJobRepository JobRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IDepartmentBudgetRepository DepartmentBudgetRepository { get; }
        IAttendanceEntryRepository AttendanceEntryRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        Task SaveChangesAsync(CancellationToken token);
        Task ExecuteTransactionAsync(Action action, CancellationToken token);
        Task ExecuteTransactionAsync(Func<Task> action, CancellationToken token);
    }
}
