namespace Workbit.Core.Models.Payment
{
    public class PaymentReadDto
    {
        public int Id { get; set; }

        public string RecipientId { get; set; } = null!;
        public string RecipientName { get; set; } = null!;

        public DateTime PaymentDate { get; set; }
		public decimal Salary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Taxes { get; set; }

        public decimal NetPay => Salary + Bonus - Taxes;

        public string? Notes { get; set; }
    }
}
