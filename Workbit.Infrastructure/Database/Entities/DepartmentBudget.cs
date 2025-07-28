using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workbit.Infrastructure.Database.Entities
{
	public class DepartmentBudget
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey(nameof(Department))]
		public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Precision(18, 2)]
		public decimal TotalBudget { get; set; }

		[Precision(18, 2)]
		public decimal BonusPool { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime DateAllocated { get; set; }
		public bool IsDistributed { get; set; } = false; // Track if manager finished distribution
	}
}
