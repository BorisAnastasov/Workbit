using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Workbit.Common.DataConstants.Manager;

namespace Workbit.Infrastructure.Database.Entities.Account
{
	public class Manager
    {
        public Manager()
        {
            this.DepartmentManagers = new List<DepartmentManager>();
        }

        [Key]
		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(OfficePhoneMaxLen)]
		public string OfficePhone { get; set; } = null!;

		public bool IsCeo { get; set; } = false;

		public virtual List<DepartmentManager> DepartmentManagers { get; set; }
    }
}
