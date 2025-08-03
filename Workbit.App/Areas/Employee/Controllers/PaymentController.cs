using Microsoft.AspNetCore.Mvc;
using Workbit.App.Extensions;
using Workbit.Core.Interfaces;

namespace Workbit.App.Areas.Employee.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService _paymentService)
        {
            this.paymentService = _paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> History(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var payments = await paymentService.GetAllByEmployeeIdAsync(User.Id(), startDate, endDate);

                return View(payments);
            }
            catch (Exception)
            {
                return RedirectToAction("Error500", "Error", new { area = "" });
            }

            
        }
    }
}
