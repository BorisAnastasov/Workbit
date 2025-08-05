using System.ComponentModel.DataAnnotations;
using Workbit.Core.Models.Country;
using Workbit.Infrastructure.Attributes;
using static Workbit.Common.DataConstants.ApplicationUser;

namespace Workbit.Core.Models.User
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

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "PhoneNumber")]
        [StringLength(PhoneMaxLen, MinimumLength = PhoneMinLen)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Display(Name = "CountryCode")]
        [StringLength(2)]
        public string CountryCode { get; set; } = null!;

        [Required]
        [Display(Name = "IBAN")]
        [ValidIban]
        public string IBAN { get; set; } = null!;

        public IEnumerable<CountrySummaryModel> Countries { get; set; } = new List<CountrySummaryModel>();
    }
}
