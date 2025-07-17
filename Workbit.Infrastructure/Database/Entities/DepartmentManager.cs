using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Entities
{
	public class DepartmentManager
	{
		public Guid ManagerId { get; set; }
		public virtual Manager Manager { get; set; } = null!;

		public int DepartmentId { get; set; }
		public virtual Department Department { get; set; } = null!;
	}

}
