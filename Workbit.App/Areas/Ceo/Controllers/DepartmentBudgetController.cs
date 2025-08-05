using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.DepartmentBudget;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class DepartmentBudgetController : BaseController
    {
        private readonly IDepartmentBudgetService departmentBudgetService;

        public DepartmentBudgetController(IDepartmentBudgetService _departmentBudgetService)
        {
            departmentBudgetService = _departmentBudgetService;
        }

        [HttpPost]
        public async Task<IActionResult> AllocateDepartmentBudget(DepartmentBudgetFormModel model)
        {
            try
            {
                await departmentBudgetService.AllocateDepartmentBudgetAsync(model);

                return RedirectToAction("Dashboard", "Payment", new { allocated = true, area="Ceo" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area="" });
            }
        }
    }
}
