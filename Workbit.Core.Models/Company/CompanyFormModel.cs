using System.ComponentModel.DataAnnotations;
using Workbit.Core.Models.Country;
using static Workbit.Common.DataConstants.Company;
using static Workbit.Common.MessageConstants;

namespace Workbit.Core.Models.Company
{
    public class CompanyFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(NameMaxLen, MinimumLength = NameMinLen, ErrorMessage = LengthMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(AddressMaxLen, MinimumLength = AddressMinLen, ErrorMessage = LengthMessage)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [Phone]
        [StringLength(ContactPhoneMaxLen, MinimumLength = ContactPhoneMinLen, ErrorMessage = LengthMessage)]
        [RegularExpression(ContactPhoneRegex)]
        public string ContactPhone { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(2)]
        public string CountryCode { get; set; } 
        public string CeoId { get; set; } = null!;

        public IEnumerable<CountrySummaryModel> Countries { get; set; } = new List<CountrySummaryModel>();
    }
}
