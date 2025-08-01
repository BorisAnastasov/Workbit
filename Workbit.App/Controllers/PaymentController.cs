﻿using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Payment;

namespace Workbit.App.Controllers
{
    public class PaymentController : Controller
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
		public async Task<IActionResult> PaymentDashboard(bool payed = false, bool errorPaying = false
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
            if(errorPaying)
            {
                TempData["Error"] = $"Failed to process payment!";
            }

            if (allocated) 
            {
                TempData["Success"] = "Department budget allocated successfully!";
            }
            if(allocError)
            {
                TempData["Error"] = "Failed to allocate department budget!";
            }

			if (payed == true || errorPaying == true || allocated == true || allocError == true)
			{
				// Redirect to the same page without any query params
				return RedirectToAction(nameof(PaymentDashboard));
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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to process payment: {ex.Message}");
            }

            return RedirectToAction(nameof(PaymentDashboard), true);
        }

		[HttpGet]
		public async Task<IActionResult> ManagerPaymentDashboard(bool paid = false, bool error = false)
		{
			try
			{
				// Get manager and their employees
				var manager = await managerService.GetByIdAsync(User.Id());
				var employees = await employeeService.GetByDepartmentIdAsync(manager.DepartmentId!.Value);

				// Optionally, get remaining budget (if tracked)
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
				return RedirectToAction("Error500", "Error");
			}
		}

        [HttpPost]
        public async Task<IActionResult> PayEmployee(PayEmployeeDto dto)
        {
            try
            {
                await paymentService.PayEmployeeByManagerAsync(dto);
                return RedirectToAction(nameof(ManagerPaymentDashboard), new { paid = true });
            }
            catch (Exception)
            {
                // You can log the exception here if needed
                return RedirectToAction(nameof(ManagerPaymentDashboard), new { error = true });
            }
        }

		[HttpGet]
		public async Task<IActionResult> HistoryForManager(DateTime? startDate, DateTime? endDate)
		{
			try
			{
				var managerId = User.Id();

				// Get filtered employee-only payments
				var payments = await paymentService.GetAllByManagerIdAsync(managerId, startDate, endDate);

				// Pass filter values to view
				ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
				ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

				return View(payments); // Reuse your view but tailored for manager
			}
			catch (Exception)
			{
				return RedirectToAction("Error500", "Error");
			}
		}

        [HttpGet]
        public async Task<IActionResult> HistoryForEmployee(DateTime? startDate, DateTime? endDate)
        {
            var employeeId = User.Id(); // Get logged-in user

            var payments = await paymentService.GetAllByEmployeeIdAsync(employeeId, startDate, endDate);

            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(payments);
        }


    }
}
