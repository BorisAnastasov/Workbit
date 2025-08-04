using Microsoft.AspNetCore.DataProtection;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Attributes;

namespace Workbit.Infrastructure.Database.Entities.Account
{
    public class Manager
    {
        // 1) EF Core needs a parameterless ctor
        public Manager() { }

        // 2) This gets set by our interceptor or ChangeTracker hook
        private IDataProtector? _protector;
        public void SetProtector(IDataProtector protector)
            => _protector = protector;

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
            get
            {
                if (_protector == null)
                    throw new InvalidOperationException("Protector not set.");
                return string.IsNullOrEmpty(EncryptedIBAN)
                    ? ""
                    : _protector.Unprotect(EncryptedIBAN);
            }
            set
            {
                if (_protector == null)
                    throw new InvalidOperationException("Protector not set.");
                EncryptedIBAN = string.IsNullOrEmpty(value)
                    ? ""
                    : _protector.Protect(value);
            }
        }
    }
}
