using Workbit.Domain.Interfaces;
using Workbit.Domain.Interfaces.Repositories;
using Workbit.Infrastructure.Persistance;
using Workbit.Infrastructure.Repository.Repositories;

namespace Workbit.Infrastructure.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly WorkbitDbContext _context;
        public IApplicationUserRepository ApplicationUserRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IManagerRepository ManagerRepository { get; }
        public ICeoRepository CeoRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public IJobRepository JobRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IDepartmentBudgetRepository DepartmentBudgetRepository { get; }
        public IAttendanceEntryRepository AttendanceEntryRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }

        public UnitOfWork(WorkbitDbContext dbContext)
        {
            _context = dbContext;
            ApplicationUserRepository = new ApplicationUserRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
            ManagerRepository = new ManagerRepository(_context);
            CeoRepository = new CeoRepository(_context);
            DepartmentRepository = new DepartmentRepository(_context);
            JobRepository = new JobRepository(_context);
            PaymentRepository = new PaymentRepository(_context);
            DepartmentBudgetRepository = new DepartmentBudgetRepository(_context);
            AttendanceEntryRepository = new AttendanceEntryRepository(_context);
            RefreshTokenRepository = new RefreshTokenRepository(_context);
        }

        public async Task SaveChangesAsync(CancellationToken token)
                    => await _context.SaveChangesAsync(token);

        public async Task ExecuteTransactionAsync(Action action, CancellationToken token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(token);
            try
            {
                action();
                await _context.SaveChangesAsync(token);
                await transaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(token);
                //handle exception
            }
        }

        public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(token);
            try
            {
                await action();
                await _context.SaveChangesAsync(token);
                await transaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(token);
                //throw TransactionException.TransactionNotExecuteException(ex);
            }
        }

    }
}
