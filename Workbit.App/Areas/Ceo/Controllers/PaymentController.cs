using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Payment;
using Workbit.Core.Services;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;
        private readonly IManagerService managerService;
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly IDepartmentBudgetService departmentBudgetService;
        private readonly ICeoService ceoService;

        public PaymentController(IPaymentService _paymentService,
                                IManagerService _managerService,
                                IDepartmentService _departmentService,
                                IEmployeeService _employeeService,
                                IDepartmentBudgetService _departmentBudgetService,
                                ICeoService ceoService)
        {
            paymentService = _paymentService;
            managerService = _managerService;
            departmentService = _departmentService;
            employeeService = _employeeService;
            departmentBudgetService = _departmentBudgetService;
            this.ceoService = ceoService;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard(bool payed = false, bool errorPaying = false
                                                  ,bool allocated = false, bool allocError = false)
        {

            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }

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


                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> History(DateTime? startDate, DateTime? endDate, string role = "All")
        {
            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }

                var payments = await paymentService.GetAllByCeoIdAsync(User.Id(), startDate, endDate, role);

                ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
                ViewBag.Role = role;

                return View(payments);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PayManager(PayManagerViewModel model)
        {
            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }

                await paymentService.PayManagerAsync(model);

                return RedirectToAction(nameof(Dashboard), new { payed = true, area = "Ceo"});
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }
    }
}
