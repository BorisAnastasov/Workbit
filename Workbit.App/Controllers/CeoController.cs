using Microsoft.AspNetCore.Mvc;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Ceo;

namespace Workbit.App.Controllers
{
    public class CeoController : Controller
    {
		private readonly IEmployeeService _employeeService;
		private readonly IManagerService _managerService;
		private readonly IDepartmentService _departmentService;
		private readonly IAttendanceService _attendanceService;
		private readonly IPaymentService _paymentService;

		public CeoController(
			IEmployeeService employeeService,
			IManagerService managerService,
			IDepartmentService departmentService,
			IAttendanceService attendanceService,
			IPaymentService paymentService)
		{
			_employeeService = employeeService;
			_managerService = managerService;
			_departmentService = departmentService;
			_attendanceService = attendanceService;
			_paymentService = paymentService;
		}

		public async Task<IActionResult> Dashboard()
		{
			// Employees & Departments
			var employees = await _employeeService.GetAllAsync();
			var managers = await _managerService.GetAllAsync();
			var departments = await _departmentService.GetAllAsync();

			int totalEmployees = employees.Count();
			int totalManagers = managers.Count();
			int totalDepartments = departments.Count();

			// Attendance Today
			var todayAttendance = await _attendanceService.GetByDateAsync(DateTime.Today);
			int presentToday = todayAttendance.Count();
			int absentToday = totalEmployees - presentToday;

			// Payroll Snapshot (this month)
			var payments = employees
				.SelectMany(e => _paymentService.GetByEmployeeIdAsync(e.Id).Result) // Synchronous wait (replace with proper async aggregation later)
				.Where(p => p.PaymentDate.Month == DateTime.Now.Month && p.PaymentDate.Year == DateTime.Now.Year)
				.ToList();

			decimal totalPayroll = payments.Sum(p => p.NetPay);
			int paidEmployees = payments.Select(p => p.EmployeeId).Distinct().Count();

			// Pass data to View
			var viewModel = new CeoDashboardViewModel
			{
				TotalEmployees = totalEmployees,
				TotalManagers = totalManagers,
				TotalDepartments = totalDepartments,
				PresentToday = presentToday,
				AbsentToday = absentToday,
				TotalPayrollThisMonth = totalPayroll,
				PaidEmployeesThisMonth = paidEmployees
			};

			return View(viewModel);
		}

		
	}
}
