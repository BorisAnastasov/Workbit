namespace Workbit.Core.Models.Payment
{
    public class PaymentSummaryDto
    {
        public DateTime PaymentDate { get; set; }
        public decimal Salary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Taxes { get; set; }
        public decimal NetPay => Salary + Bonus - Taxes;
    }
}
