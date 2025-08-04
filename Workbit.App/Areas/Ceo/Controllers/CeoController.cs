using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Ceo;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class CeoController : Base
    {
        private readonly IEmployeeService employeeService;
        private readonly IManagerService managerService;
        private readonly IDepartmentService departmentService;
        private readonly IAttendanceService attendanceService;
        private readonly IPaymentService paymentService;
        private readonly ICeoService ceoService;

        public CeoController(
            IEmployeeService employeeService,
            IManagerService managerService,
            IDepartmentService departmentService,
            IAttendanceService attendanceService,
            IPaymentService paymentService,
            ICeoService ceoService)
        {
            this.employeeService = employeeService;
            this.managerService = managerService;
            this.departmentService = departmentService;
            this.attendanceService = attendanceService;
            this.paymentService = paymentService;
            this.ceoService = ceoService;
        }

        public async Task<IActionResult> Dashboard()
        {
            // Employees & Departments
            var employees = await employeeService.GetAllByCeoIdAsync();
            var managers = await managerService.GetAllAsync();
            var departments = await departmentService.GetAllAsync();

            var ceo = await ceoService.GetByUserIdAsync(User.Id());

            int totalEmployees = employees.Count();
            int totalManagers = managers.Count();
            int totalDepartments = departments.Count();

            // Attendance Today
            var todayAttendance = await attendanceService.GetByDateAsync(DateTime.Today);
            int presentToday = todayAttendance.Count();
            int absentToday = totalEmployees - presentToday;

            // Payroll Snapshot (this month)
            var payments = employees
                .SelectMany(e => paymentService.GetByEmployeeIdAsync(e.Id).Result) // Synchronous wait (replace with proper async aggregation later)
                .Where(p => p.PaymentDate.Month == DateTime.Now.Month && p.PaymentDate.Year == DateTime.Now.Year)
                .ToList();

            decimal totalPayroll = payments.Sum(p => p.NetPay);
            int paidEmployees = payments.Select(p => p.RecipientId).Distinct().Count();

            // Pass data to View
            var viewModel = new CeoDashboardViewModel
            {
                CopmanyId = ceo.CompanyId!.Value,
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
