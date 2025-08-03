namespace Workbit.Core.Models.Employee
{
    public class EmployeePaymentViewModel
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public decimal Salary { get; set; }
        public string IBAN { get; set; }
    }
}
