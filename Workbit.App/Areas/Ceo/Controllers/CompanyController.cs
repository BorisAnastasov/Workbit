using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Company;

namespace Workbit.App.Areas.Ceo.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService companyService;
        private readonly ICeoService ceoService;
        private readonly IUserService userService;

        public CompanyController(ICompanyService companyService, ICeoService ceoService, IUserService userService)
        {
            this.companyService = companyService;
            this.ceoService = ceoService;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await ceoService.HasCompanyByIdAsync(User.Id()))
            {
                return RedirectToAction(nameof(NoCompany), "Base", new { area = "Ceo" });
            }

            try
            {
                if (!await companyService.ExistsByIdAsync(id))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                await companyService.DeleteAsync(id);

                return RedirectToAction("Index", "Home", new { area = "" });
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
                if (await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                var model = new CompanyFormModel
                {
                    Countries = await userService.GetCountries(),
                    CeoId = User.Id()
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyFormModel model)
        {
            try
            {
                if (await ceoService.HasCompanyByIdAsync(User.Id()))
                {
                    return RedirectToAction("Error404", "Error", new { area = "" });
                }

                if (!ModelState.IsValid)
                {
                    model.Countries = await userService.GetCountries();
                    return View(model);
                }

                await companyService.CreateAsync(model);
                
                return RedirectToAction("Profile", "Ceo", new {area="Ceo" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }
        }
    }
}
