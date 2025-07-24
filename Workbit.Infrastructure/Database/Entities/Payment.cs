using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; } 
        public virtual Employee Employee { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }

		[Precision(18, 2)]
		public decimal Salary { get; set; }

		[Precision(18, 2)]
		public decimal Bonus { get; set; }

		[Precision(18, 2)]
		public decimal Taxes { get; set; }

        public decimal NetPay => Salary + Bonus - Taxes;

        public string? Notes { get; set; }
    }

}
