using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Entities
{
    public class SalaryPayment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; } 
        public virtual Employee Employee { get; set; } = null!;

        public DateTime DateOfPayment { get; set; }

        public decimal Payment { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }

        public decimal NetPay => Payment + Bonus - Deduction;

        public string? Notes { get; set; }
    }

}
