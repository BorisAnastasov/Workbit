using System.ComponentModel.DataAnnotations;
using static Workbit.Common.MessageConstants;
using static Workbit.Common.DataConstants.Department;

namespace Workbit.Core.Models.Department
{
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(NameMaxLen, MinimumLength = NameMinLen, ErrorMessage = LengthMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen, ErrorMessage = LengthMessage)]
        public string Description { get; set; } = string.Empty;
    }
}
