using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;

        public virtual List<DepartmentManager> DepartmentManagers { get; set; }

        public virtual List<Job> Jobs { get; set; }
    }
}
