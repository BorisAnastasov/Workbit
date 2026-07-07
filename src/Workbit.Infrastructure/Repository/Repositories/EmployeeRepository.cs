using Workbit.Domain.Entities.Account;
using Workbit.Domain.Interfaces.Repositories;
using Workbit.Infrastructure.Database.Repository;
using Workbit.Infrastructure.Persistance;

namespace Workbit.Infrastructure.Repository.Repositories
{
    public class EmployeeRepository(WorkbitDbContext context)
        : Repository<Employee>(context), IEmployeeRepository
    {
    }
}
