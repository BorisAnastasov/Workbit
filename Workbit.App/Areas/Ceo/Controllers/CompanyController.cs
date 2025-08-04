using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Company;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!await companyService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error");
                }

                await companyService.DeleteAsync(id);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CompanyCreateDto
            {
                CeoId = User.Id()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateDto model)
        {
            try
            {
                await companyService.CreateAsync(model);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                return RedirectToAction("Dashboard", "Ceo");
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error");
            }
        }
    }
}
