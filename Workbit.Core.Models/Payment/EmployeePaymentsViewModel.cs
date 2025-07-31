namespace Workbit.Core.Models.Payment
{
    public class EmployeePaymentsViewModel
    {
        public IEnumerable<PaymentReadDto> Payments { get; set; }
        public string? StartDate { get; set; } 
        public string? EndDate { get; set; }

    }
}
