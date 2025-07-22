using Microsoft.EntityFrameworkCore;
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

        [DataType(DataType.DateTime)]
        public DateTime DateOfPayment { get; set; }

		[Precision(18, 2)]
		public decimal Payment { get; set; }

		[Precision(18, 2)]
		public decimal Bonus { get; set; }

		[Precision(18, 2)]
		public decimal Deduction { get; set; }

        public decimal NetPay => Payment + Bonus - Deduction;

        public string? Notes { get; set; }
    }

}
