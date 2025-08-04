using Microsoft.AspNetCore.DataProtection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Attributes;
using Workbit.Infrastructure.Enumerations;
using Workbit.Infrastructure.Extensions;

namespace Workbit.Infrastructure.Database.Entities.Account
{
    public class Employee
    {
        [NotMapped]
        private IDataProtector? protector;

        public void SetProtector(IDataProtector protector)
        {
            this.protector = protector;
        }

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

        public string EncryptedIBAN { get; set; } = null!;

        [ValidIban]
        [NotMapped]
        public string IBAN
        {
            get => protector == null
                ? throw new InvalidOperationException("Protector not set.")
                : string.IsNullOrEmpty(EncryptedIBAN) ? "" : protector.Unprotect(EncryptedIBAN);

            set
            {
                if (protector == null)
                    throw new InvalidOperationException("Protector not set.");

                EncryptedIBAN = string.IsNullOrEmpty(value) ? "" : protector.Protect(value);
            }
        }

        [NotMapped]
        public decimal EffectiveSalary =>
                        this.Job == null ? 0 : this.Job.BaseSalary * Level.GetMultiplier();

    }
}
