using System.ComponentModel.DataAnnotations;
using static Workbit.Common.DataConstants.ApplicationUser;

namespace Workbit.Core.Models.Account
{
	public class RegisterEmployeeViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        [StringLength(FirstNameMaxLen, MinimumLength = FirstNameMinLen)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(LastNameMaxLen, MinimumLength = LastNameMinLen)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}
