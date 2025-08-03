using static Workbit.Common.MessageConstants;
using static Workbit.Common.DataConstants.Job;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Workbit.Core.Models.Job
{
    public class JobEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(TitleMaxLen, MinimumLength = TitleMinLen, ErrorMessage = LengthMessage)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen, ErrorMessage = LengthMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [Precision(18, 2)]
        public decimal BaseSalary { get; set; }
    }
}
