using Microsoft.AspNetCore.DataProtection;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Attributes;

namespace Workbit.Infrastructure.Database.Entities.Account
{
    public class Manager
    {
        [NotMapped]
        private IDataProtector? protector;
        public void SetProtector(IDataProtector protector)
            => this.protector = protector;

        [Key]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

        // 3) The only property EF maps to the DB
        public string EncryptedIBAN { get; set; } = null!;

        // 4) Our decrypted view, never mapped
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
    }
}
