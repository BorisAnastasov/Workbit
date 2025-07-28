namespace Workbit.Core.Models.Payment
{
    public class PayManagerViewModel
    {
        public string ManagerId { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Taxes { get; set; }
        public string? Notes { get; set; }

    }
}
