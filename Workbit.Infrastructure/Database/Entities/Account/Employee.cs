using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Enumerations;
using Workbit.Infrastructure.Extensions;

namespace Workbit.Infrastructure.Database.Entities.Account
{
    public class Employee
    {
        [Key, ForeignKey(nameof(ApplicationUser))]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

		[ForeignKey(nameof(Job))]
        public int? JobId { get; set; }
        public virtual Job Job { get; set; }

		[Required]
		public JobLevel Level { get; set; } = JobLevel.Unemployed;

        [NotMapped]
        public decimal EffectiveSalary =>
                        this.Job == null ? 0 : this.Job.BaseSalary * Level.GetMultiplier();

    }
}
