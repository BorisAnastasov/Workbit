using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static Workbit.Common.DataConstants.Payment;
using static Workbit.Common.MessageConstants;

namespace Workbit.Core.Models.Payment
{
    public class PaymentUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Precision(18, 2)]
        public double Salary { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Precision(18, 2)]
        public double Bonus { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Precision(18, 2)]
        public double Taxes { get; set; }

        [StringLength(NotesMaxLen, ErrorMessage = LengthMessage)]
        public string? Notes { get; set; }
    }
}
