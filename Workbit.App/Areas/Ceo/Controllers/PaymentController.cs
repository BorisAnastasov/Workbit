using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Payment;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;
        private readonly IManagerService managerService;
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly IDepartmentBudgetService departmentBudgetService;

        public PaymentController(IPaymentService _paymentService, IManagerService _managerService, IDepartmentService _departmentService, IEmployeeService _employeeService, IDepartmentBudgetService _departmentBudgetService)
        {
            paymentService = _paymentService;
            managerService = _managerService;
            departmentService = _departmentService;
            employeeService = _employeeService;
            departmentBudgetService = _departmentBudgetService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard(bool payed = false, bool errorPaying = false
                                                                            , bool allocated = false, bool allocError = false)
        {
            var managers = await managerService.GetAllByCeoIdAsync(User.Id());
            var departments = await departmentService.GetAllByCeoIdAsync(User.Id());

            var model = new PaymentDashboardViewModel
            {
                CeoId = User.Id(),
                Managers = managers,
                Departments = departments,
            };

            if (payed)
            {
                TempData["Success"] = $"Manager was paid successfully!";
            }
            if (errorPaying)
            {
                TempData["Error"] = $"Failed to process payment!";
            }

            if (allocated)
            {
                TempData["Success"] = "Department budget allocated successfully!";
            }
            if (allocError)
            {
                TempData["Error"] = "Failed to allocate department budget!";
            }

            if (payed == true || errorPaying == true || allocated == true || allocError == true)
            {
                // Redirect to the same page without any query params
                return RedirectToAction(nameof(Dashboard));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> History(DateTime? startDate, DateTime? endDate, string role = "All")
        {
            var ceoId = User.Id();

            // Get filtered payments
            var payments = await paymentService.GetAllByCeoIdAsync(ceoId, startDate, endDate, role);

            // Keep filter values for the view (so form keeps selected state)
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.Role = role;

            return View(payments);
        }


        [HttpPost]
        public async Task<IActionResult> PayManager(PayManagerViewModel model)
        {
            try
            {
                await paymentService.PayManagerAsync(model);

                return RedirectToAction(nameof(Dashboard), true);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }

            
        }


    }
}
