using Workbit.Core.Models.DepartmentBudget;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Core.Interfaces
{
    public interface IDepartmentBudgetService
    {
        Task AllocateDepartmentBudgetAsync(DepartmentBudgetFormModel model);
        Task<DepartmentBudget?> GetLatestByDepartmentIdAsync(int departmentId);
    }
}
