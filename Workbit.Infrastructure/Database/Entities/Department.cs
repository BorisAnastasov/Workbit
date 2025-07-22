using System.ComponentModel.DataAnnotations;
using static Workbit.Common.DataConstants.Department;

namespace Workbit.Infrastructure.Database.Entities
{
	public class Department
    {
        public Department()
        {
            this.Jobs = new List<Job>();
            this.DepartmentManagers = new List<DepartmentManager>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLen)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLen)]
        public string Description { get; set; } = null!;

        public virtual List<DepartmentManager> DepartmentManagers { get; set; }

        public virtual List<Job> Jobs { get; set; }
    }
}
