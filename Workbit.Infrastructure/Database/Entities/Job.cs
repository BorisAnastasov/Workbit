using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Extensions;
using Workbit.Core.Enumerations;
using static Workbit.Common.DataConstants.Job;
using Workbit.Infrastructure.Database.Entities.Account;

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
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

        [Required]  
        public JobLevel Level { get; set; }

        [Required]
        public decimal BaseSalary { get; set; }

        [NotMapped]
        public decimal EffectiveSalary => BaseSalary * Level.GetMultiplier();

        public virtual List<Employee> Employees { get; set; }
    }
}
