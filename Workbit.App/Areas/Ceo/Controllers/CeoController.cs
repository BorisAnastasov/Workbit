using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Ceo;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class CeoController : BaseController
    {
        private readonly IEmployeeService employeeService;
        private readonly IManagerService managerService;
        private readonly IDepartmentService departmentService;
        private readonly IAttendanceService attendanceService;
        private readonly IPaymentService paymentService;
        private readonly ICeoService ceoService;
        private readonly ICompanyService companyService;

        public CeoController(
            IEmployeeService employeeService,
            IManagerService managerService,
            IDepartmentService departmentService,
            IAttendanceService attendanceService,
            IPaymentService paymentService,
            ICeoService ceoService,
            ICompanyService companyService)
        {
            this.employeeService = employeeService;
            this.managerService = managerService;
            this.departmentService = departmentService;
            this.attendanceService = attendanceService;
            this.paymentService = paymentService;
            this.ceoService = ceoService;
            this.companyService = companyService;
        }

        public async Task<IActionResult> Profile()
        {
            try
            {
                if (!await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
                }
                var ceoId = User.Id();

                // Employees & Departments
                var employees = await employeeService.GetAllByCeoIdAsync(ceoId);
                var managers = await managerService.GetAllbyCeoIdAsync(ceoId);
                var departments = await departmentService.GetAllByCeoIdAsync(ceoId);

                var company = await companyService.GetByCeoIdAsync(User.Id());

                int totalEmployees = employees.Count();
                int totalManagers = managers.Count();
                int totalDepartments = departments.Count();

                // Attendance Today
                var todayAttendance = await attendanceService.GetByDateAsync(DateTime.Today, ceoId);
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
                    CompanyId = company.Id,
                    CompanyName = company.Name,
                    TotalEmployees = totalEmployees,
                    TotalManagers = totalManagers,
                    TotalDepartments = totalDepartments,
                    PresentToday = presentToday,
                    AbsentToday = absentToday,
                    TotalPayrollThisMonth = totalPayroll,
                    PaidEmployeesThisMonth = paidEmployees,
                    CeoName = company.Name,
                    ContactPhone = company.ContactPhone,
                    CountryName = company.Country,
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }
    }
}
