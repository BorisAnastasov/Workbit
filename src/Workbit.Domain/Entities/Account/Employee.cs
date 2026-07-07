using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Domain.Attributes;
using Workbit.Domain.Enumerations;
using Workbit.Domain.Extensions;

namespace Workbit.Domain.Entities.Account
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
        public virtual Country Country { get; set; } = null!;

        [ValidIban]
        [Required]
        public string Iban { get; set; } = null!;

        [NotMapped]
        public decimal EffectiveSalary =>
            Job == null ? 0 : Job.BaseSalary * Level.GetMultiplier();
    }
}