using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Payment;

namespace Workbit.App.Areas.Manager.Controllers
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
		public async Task<IActionResult> Dashboard(bool paid = false, bool error = false)
		{
			try
			{

                if (!await managerService.HasDepartmentByManagerIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoDepartment), "Base", new { area = "Manager" });
                }
                var manager = await managerService.GetByIdAsync(User.Id());
				var employees = await paymentService.GetEmployeePaymentModelByDepartmentIdAsync(manager.DepartmentId!.Value);

				var budget = await departmentBudgetService.GetLatestByDepartmentIdAsync(manager.DepartmentId.Value);
				var remaining = budget!.TotalBudget;

				var model = new ManagerPaymentDashboardViewModel
				{
					ManagerId = User.Id(),
					DepartmentId = manager.DepartmentId!.Value,
					Employees = employees.ToList(),
					TotalBudget = remaining,
				};

				if (paid) TempData["Success"] = "Employee payment sent successfully!";
				if (error) TempData["Error"] = "Failed to process payment.";

				return View(model);
			}
			catch
			{
				return RedirectToAction("Error500", "Error", new {area="" });
			}
		}

        [HttpPost]
        public async Task<IActionResult> PayEmployee(PayEmployeeFormModel model)
        {
            //try
            {
                if (!await managerService.HasDepartmentByManagerIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoDepartment), "Base", new { area = "Manager" });
                }

                await paymentService.PayEmployeeByManagerAsync(model);

                return RedirectToAction(nameof(Dashboard), new { paid = true });
            }
            //catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

		[HttpGet]
		public async Task<IActionResult> History(DateTime? startDate, DateTime? endDate)
		{
			try
			{

                if (!await managerService.HasDepartmentByManagerIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoDepartment), "Base", new { area = "Manager" });
                }
                var managerId = User.Id();

				var payments = await paymentService.GetAllByManagerIdAsync(managerId, startDate, endDate);

				ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
				ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

				return View(payments);
			}
			catch (Exception)
			{
				return RedirectToAction("Error500", "Error", new { area = "" });
            }
		}


    }
}
