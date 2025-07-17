using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workbit.Infrastructure.Database.Entities.Account
{
    public class Employee
    {
        public Employee()
        {
            this.SalaryPayments = new List<SalaryPayment>();
            this.Attendances = new List<Attendance>();
        }
        [Key]
		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; } = null!;

		[ForeignKey(nameof(Job))]
        public int JobId { get; set; }
        public virtual Job Job { get; set; } = null!;

        public virtual List<SalaryPayment> SalaryPayments { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
	}
}
