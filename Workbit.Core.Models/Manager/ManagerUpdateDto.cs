using System.ComponentModel.DataAnnotations;
using static Workbit.Common.MessageConstants;
using static Workbit.Common.DataConstants.Manager;

namespace Workbit.Core.Models.Manager
{
    public class ManagerUpdateDto
    {
        public string Id { get; set; } = null!;
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = RequiredMessage)]
        [Phone]
        [StringLength(OfficePhoneMaxLen, MinimumLength = OfficePhoneMinLen, ErrorMessage = LengthMessage)]
        [RegularExpression(OfficePhoneRegex)]
        public string OfficePhone { get; set; } = null!;
        public bool IsCeo { get; set; }
    }
}
