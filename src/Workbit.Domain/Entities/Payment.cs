using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Domain.Entities.Account;
using static Workbit.Domain.Constants.DataConstants.Payment;

namespace Workbit.Domain.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Recipient))]
        public Guid RecipientId { get; set; }
        public virtual ApplicationUser Recipient { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }

        [Precision(DecimalPrecision, DecimalScale)]
        public decimal Salary { get; set; }

        [Precision(DecimalPrecision, DecimalScale)]
        public decimal Bonus { get; set; }

        [Precision(DecimalPrecision, DecimalScale)]
        public decimal Taxes { get; set; }

        [StringLength(NotesMaxLen)]
        public string? Notes { get; set; }
    }
}
