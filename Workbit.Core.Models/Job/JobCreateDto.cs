using static Workbit.Common.MessageConstants;
using static Workbit.Common.DataConstants.Job;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Workbit.Core.Models.Job
{
    public class JobCreateDto
    {

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(TitleMaxLen, MinimumLength = TitleMinLen, ErrorMessage = LengthMessage)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen, ErrorMessage = LengthMessage)]
        public string Description { get; set; } = null!;
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Precision(18, 2)]
        public decimal BaseSalary { get; set; }
    }
}
