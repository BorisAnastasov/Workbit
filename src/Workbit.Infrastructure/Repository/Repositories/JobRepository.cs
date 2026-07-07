using Workbit.Domain.Entities;
using Workbit.Domain.Interfaces.Repositories;
using Workbit.Infrastructure.Database.Repository;
using Workbit.Infrastructure.Persistance;

namespace Workbit.Infrastructure.Repository.Repositories
{
    public class JobRepository(WorkbitDbContext context)
        :Repository<Job>(context), IJobRepository
    {
    }
}
