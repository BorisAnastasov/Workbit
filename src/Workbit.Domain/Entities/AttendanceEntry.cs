using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Domain.Entities.Account;
using Workbit.Domain.Enumerations;

namespace Workbit.Domain.Entities
{
    public class AttendanceEntry
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public EntryType Type { get; set; }
    }
}
