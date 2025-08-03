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

        


    }
}
