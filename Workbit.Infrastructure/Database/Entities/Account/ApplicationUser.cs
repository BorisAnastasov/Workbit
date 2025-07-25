using Microsoft.AspNetCore.Identity;
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

        public virtual List<AttendanceEntry> AttendanceEntries { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(PhoneMaxLen)]
        public override string PhoneNumber { get; set; } = null!;

        [NotMapped]
        public string FullName => FirstName + " " + LastName;


    }
}
