using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.DepartmentBudget;

namespace Workbit.App.Controllers
{
    public class DepartmentBudgetController : Controller
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

                return RedirectToAction("PaymentDashboard", "Payment", new { allocated = true });
            }
            catch (Exception)
            {
                return RedirectToAction("PaymentDashboard", "Payment", new { allocError = true });
            }
        }
    }
}
