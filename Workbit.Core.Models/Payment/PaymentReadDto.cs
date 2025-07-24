namespace Workbit.Core.Models.Payment
{
    public class PaymentReadDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public decimal Salary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Taxes { get; set; }
        public decimal NetPay => Salary + Bonus - Taxes;
        public string? Notes { get; set; }
    }
}
