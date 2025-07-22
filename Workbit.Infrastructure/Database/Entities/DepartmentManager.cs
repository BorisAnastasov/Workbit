using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Entities
{
	public class DepartmentManager
	{
		[ForeignKey(nameof(Manager))]
		public Guid ManagerId { get; set; }
		public virtual Manager Manager { get; set; } = null!;

		[ForeignKey(nameof(Department))]
		public int DepartmentId { get; set; }
		public virtual Department Department { get; set; } = null!;
	}

}
