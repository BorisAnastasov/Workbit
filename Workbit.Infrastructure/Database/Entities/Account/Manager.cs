using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public string OfficePhone { get; set; } = null!;

        public List<DepartmentManager> DepartmentManagers { get; set; }
    }
}
