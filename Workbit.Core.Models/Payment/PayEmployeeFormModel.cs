namespace Workbit.Core.Models.Payment
{
    public class PayEmployeeFormModel
    {
        public Guid ManagerId { get; set; }      // Manager processing the payment
        public Guid EmployeeId { get; set; }     // Employee receiving the payment

        public decimal Salary { get; set; }      // Base salary to pay
        public decimal Bonus { get; set; }       // Optional bonus

        public string? Notes { get; set; }       // Optional payment notes
    }
}
