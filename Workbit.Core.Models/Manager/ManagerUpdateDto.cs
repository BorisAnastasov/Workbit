using System.ComponentModel.DataAnnotations;
using static Workbit.Common.MessageConstants;
using static Workbit.Common.DataConstants.ApplicationUser;

namespace Workbit.Core.Models.Manager
{
    public class ManagerUpdateDto
    {
        public string Id { get; set; } = null!;
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = RequiredMessage)]
        [Phone]
        [StringLength(PhoneMaxLen, MinimumLength = PhoneMaxLen, ErrorMessage = LengthMessage)]
        public string PhoneNumber { get; set; } = null!;
    }
}
