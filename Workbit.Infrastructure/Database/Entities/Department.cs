using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Database.Entities.Account;
using static Workbit.Common.DataConstants.Department;

namespace Workbit.Infrastructure.Database.Entities
{
	public class Department
    {
        public Department()
        {
            this.Jobs = new List<Job>();
            this.Managers = new List<Manager>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLen)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLen)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = null!;

        public virtual List<Manager> Managers { get; set; } = null!;

        public virtual List<Job> Jobs { get; set; } = null!;
    }
}
