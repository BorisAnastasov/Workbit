using Workbit.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.DepartmentBudget;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Core.Services
{
    public class DepartmentBudgetService : IDepartmentBudgetService
    {
        private readonly IRepository repository;

        public DepartmentBudgetService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AllocateDepartmentBudgetAsync(DepartmentBudgetFormModel model)
        {
            var departmentBudget = new DepartmentBudget()
            {
                DepartmentId = model.DepartmentId,
                TotalBudget = model.TotalBudget,
                BonusPool = model.BonusPool,
                DateAllocated = DateTime.Now,
            };

            await repository.AddAsync(departmentBudget);
            await repository.SaveChangesAsync();
        }

        public async Task<DepartmentBudget?> GetLatestByDepartmentIdAsync(int departmentId)
        {
            return await repository.AllReadOnly<DepartmentBudget>()
                .Where(b => b.DepartmentId == departmentId)
                .OrderByDescending(b => b.DateAllocated)
                .FirstOrDefaultAsync();
        }
    }
}
