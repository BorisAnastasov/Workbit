using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Job;

namespace Workbit.App.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService jobService;
        private readonly IManagerService managerService;

        public JobController(IJobService _jobService, IManagerService _managerService)
        {
            jobService = _jobService;
            managerService = _managerService;
        }

        [HttpGet]
        public async Task<IActionResult> JobDetails(int id)
        {
            var job = await jobService.GetByIdAsync(id); // Returns JobReadDto

            return View(job); // Pass JobReadDto to JobDetails.cshtml
        }

        [HttpGet]
        public async Task<IActionResult> JobDetailsForManager(int id)
        {
            try
            {
                // Get the job details
                if (!await jobService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error");
                }

                // Check if the logged-in manager can access this job
                var manager = await managerService.GetByIdAsync(User.Id());
                var job = await jobService.GetByIdAsync(id);
                if (manager?.DepartmentId != job.DepartmentId)
                {
                    return RedirectToAction("Error403", "Error");
                }
                var jobDetails = await jobService.GetByIdAsync(id);
                return View(jobDetails); 
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await jobService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error");
                }

                var job = await jobService.GetByIdAsync(id);

                var manager = await managerService.GetByIdAsync(User.Id());
                if (manager?.DepartmentId != job.DepartmentId)
                {
                    return RedirectToAction("Error403", "Error");
                }

                await jobService.DeleteAsync(id);

                return RedirectToAction("DepartmentJobs", "Manager");
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditJob(int id)
        {
            try
            {
                if (!await jobService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error");
                }

                var job = await jobService.GetByIdAsync(id);
                var manager = await managerService.GetByIdAsync(User.Id());

                if (manager?.DepartmentId != job.DepartmentId)
                {
                    return RedirectToAction("Error403", "Error");
                }

                var editModel = new JobUpdateDto
                {
                    Id = job.Id,
                    Title = job.Title,
                    Description = job.Description,
                    BaseSalary = (decimal)job.BaseSalary,
                };

                return View(editModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditJob(JobUpdateDto model)
        {
            try
            {
                if (!await jobService.ExistsByIdAsync(model.Id))
                {
                    return RedirectToAction("Error404", "Error");
                }

                var job = await jobService.GetByIdAsync(model.Id);
                var manager = await managerService.GetByIdAsync(User.Id());

                if (manager?.DepartmentId != job.DepartmentId)
                {
                    return RedirectToAction("Error403", "Error");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await jobService.UpdateAsync(model);

                return RedirectToAction("JobDetailsForManager", "Job", new { Id = model.Id });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateJob()
        {
            try
            {
                if (!await managerService.ExistsByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error");
                }

                var manager = await managerService.GetByIdAsync(User.Id());
                var createModel = new JobCreateDto
                {
                    DepartmentId = manager.DepartmentId!.Value
                };

                return View(createModel);
            }
            catch
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(JobCreateDto model)
        {
            try
            {
                var manager = await managerService.GetByIdAsync(User.Id());

                if (manager.DepartmentId != model.DepartmentId)
                {
                    return RedirectToAction("Error403", "Error");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                await jobService.CreateAsync(model);

                return RedirectToAction("DepartmentJobs", "Manager");
            }
            catch (Exception)
            {
                return RedirectToAction("DepartmentJobs", "Manager");
            }
        }


    }
}
