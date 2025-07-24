using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Workbit.Common.MessageConstants;
using static Workbit.Common.DataConstants.Payment;

namespace Workbit.Core.Models.Payment
{
    public class PaymentCreateDto
    {
        public string EmployeeId { get; set; } = null!;
        public DateTime PaymentDate { get; set; }

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
