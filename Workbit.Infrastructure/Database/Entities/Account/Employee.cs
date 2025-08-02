using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Attributes;
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
        public virtual Job? Job { get; set; }

		[Required]
		public JobLevel Level { get; set; } = JobLevel.Unemployed;

        [ForeignKey(nameof(Country))]
        public string? CountryCode { get; set; }
        public virtual Country Country { get; set; }

        [ValidIban]
        public string IBAN { get; set; } = null!;

        [NotMapped]
        public decimal EffectiveSalary =>
                        this.Job == null ? 0 : this.Job.BaseSalary * Level.GetMultiplier();

    }
}
