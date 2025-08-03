using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Job;

namespace Workbit.App.Areas.Manager.Controllers
{
    public class JobController : BaseController
    {
        private readonly IJobService jobService;
        private readonly IManagerService managerService;

        public JobController(IJobService _jobService, IManagerService _managerService)
        {
            jobService = _jobService;
            managerService = _managerService;
        }


        [HttpGet]
        public async Task<IActionResult> All() 
        {
            try
            {
                if (!await managerService.HasDepartmentByManagerIdAsync(User.Id()))
                {
                    return RedirectToAction("NoDepartment", "Base", new { area = "Manager" });
                }

                var departmentId = await managerService.GetDepartmentIdByManagerIdAsync(User.Id());

                var models = await jobService.GetByDepartmentIdAsync(departmentId);

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (!await jobService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error", new {area="" });
                }

                if (!await managerService.IsManagerOfJobAsync(User.Id(), id)) 
                {
                    return RedirectToAction("Error403", "Error");
                }

                var jobDetails = await jobService.GetByIdAsync(id);

                return View(jobDetails); 
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new {area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await jobService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                if (!await managerService.IsManagerOfJobAsync(User.Id(), id))
                {
                    return RedirectToAction("Error403", "Error", new { area = "" });
                }

                await jobService.DeleteAsync(id);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new {area="" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (!await jobService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                if (!await managerService.IsManagerOfJobAsync(User.Id(), id))
                {
                    return RedirectToAction("Error403", "Error", new { area = "" });
                }

                var model = await jobService.GetJobEditModelAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobEditViewModel model)
        {
            try
            {
                if (!await jobService.ExistsByIdAsync(model.Id))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                if (!await managerService.IsManagerOfJobAsync(User.Id(), model.Id))
                {
                    return RedirectToAction("Error403", "Error", new {area="" });
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await jobService.EditJobAsync(model);

                return RedirectToAction(nameof(All), "Job", new { area="Manager" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {

                var departmentId = await managerService.GetDepartmentIdByManagerIdAsync(User.Id());

                var model = new JobCreateViewModel
                {
                    DepartmentId = departmentId
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobCreateViewModel model)
        {
            try
            {
                if(!(await managerService.GetDepartmentIdByManagerIdAsync(User.Id()) == model.DepartmentId))
                {
                    return RedirectToAction("Error403", "Error", new { area = "" });
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await jobService.CreateJobAsync(model);

                return RedirectToAction(nameof(All), "Job", new {area="Manager" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }


    }
}
