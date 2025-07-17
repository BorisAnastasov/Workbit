using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Workbit.Common.DataConstants.ApplicationUser;

namespace Workbit.Infrastructure.Database.Entities.Account
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        [Required]
        [StringLength(FirstNameMaxLen)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLen)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime DateOfBirth { get; set; }


    }
}
