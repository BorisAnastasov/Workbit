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
            paymentService = _paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> History(DateTime? startDate, DateTime? endDate)
        {
            var employeeId = User.Id();

            var payments = await paymentService.GetAllByEmployeeIdAsync(employeeId, startDate, endDate);

            return View(payments);
        }
    }
}
