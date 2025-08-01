﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Workbit.Common.DataConstants.ApplicationUser;

namespace Workbit.Infrastructure.Database.Entities.Account
{
	public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            this.AttendanceEntries = new List<AttendanceEntry>();
            this.Payments = new List<Payment>();
        }

        [Required]
        [StringLength(FirstNameMaxLen)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLen)]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        [Phone]
        [StringLength(PhoneMaxLen)]
        public override string PhoneNumber { get; set; } = null!;

        public virtual Ceo? Ceo { get; set; }
        public virtual Manager? Manager { get; set; }
        public virtual Employee? Employee { get; set; }

        public virtual List<AttendanceEntry> AttendanceEntries { get; set; } = null!;
        public virtual List<Payment> Payments { get; set; } = null!;

        [NotMapped]
        public string FullName => FirstName + " " + LastName;

    }
}
