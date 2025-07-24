using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Database.Entities.Account;
using static Workbit.Common.DataConstants.Job;

namespace Workbit.Infrastructure.Database.Entities
{
	public class Job
    {
        public Job()
        {
            this.Employees = new List<Employee>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLen)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLen)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

        [Required]
		[Precision(18, 2)]
		public decimal BaseSalary { get; set; }

        public virtual List<Employee> Employees { get; set; } = null!;
    }
}
