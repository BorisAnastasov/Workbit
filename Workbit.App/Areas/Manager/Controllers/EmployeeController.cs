using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Services;

namespace Workbit.App.Areas.Manager.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService employeeService;
        private readonly IManagerService managerService;

        public EmployeeController(IEmployeeService _employeeService, IManagerService _managerService)
        {
            this.employeeService = _employeeService;
            this.managerService = _managerService;
        }

        [HttpGet]
        public async Task<IActionResult> TeamEmployees()
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error");
                }

                if (!await managerService.HasDepartmentByUserIdAsync(User.Id()))
                {
                    return RedirectToAction("NoDepartment", "Manager", new { area = "Manager" });
                }

                var manager = await managerService.GetByIdAsync(User.Id());

                var employees = await employeeService.GetByDepartmentIdAsync(manager.DepartmentId!.Value);

                return View(employees);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }


    }
}
