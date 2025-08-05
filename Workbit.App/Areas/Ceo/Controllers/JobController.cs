using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Job;

namespace Workbit.App.Areas.Ceo.Controllers
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
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (!await jobService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error", new {area="" });
                }

                var jobDetails = await jobService.GetByIdAsync(id);

                return View(jobDetails); 
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new {area = "" });
            }
        }



    }
}
